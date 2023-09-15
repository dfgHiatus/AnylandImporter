using AnylandImporter.Converters;
using AnylandImporter.Tests;
using BaseX;
using CodeX;
using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AnylandImporter;

public class Importer : NeosMod
{
    public override string Name => "AnylandImporter";

    public override string Author => "dfgHiatus";

    public override string Version => "1.0.0";

    public static ModConfiguration config;
    public const string AnylandWorldExtension = ".anyland";
    internal static readonly string CachePath = Path.Combine(Engine.Current.AppPath, "nml_mods", "AnylandImporter");

    public override void OnEngineInit()
    {
        new Harmony("net.dfgHiatus.AnylandImporter").PatchAll();
        config = GetConfiguration();
    }


    [HarmonyPatch(typeof(UniversalImporter), "Import", typeof(AssetClass), typeof(IEnumerable<string>),
        typeof(World), typeof(float3), typeof(floatQ), typeof(bool))]
    public class UniversalImporterPatch
    {
        static bool Prefix(ref IEnumerable<string> files)
        {
            List<string> hasAnylandWorld = new();
            List<string> notAnylandWorld = new();
            foreach (var file in files)
            {
                if (Path.GetExtension(file).ToLower() == AnylandWorldExtension)
                    hasAnylandWorld.Add(file);
                else
                    notAnylandWorld.Add(file);
            }

            var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot("Anyland World Import");
            slot.PositionInFrontOfUser();

            foreach (var file in hasAnylandWorld)
            {
                slot.StartGlobalTask(async delegate
                {
                    Area area;
                    try
                    {
                        area = JsonConvert.DeserializeObject<Area>(File.ReadAllText(file));
                    }
                    catch (Exception e)
                    {
                        Error($"Failed to deserialize Area {file}: {e.Message}");
                        return;
                    }

                    if (area == null)
                    {
                        Error($"Deserialized Area, but file was invalid: {file}");
                        return;
                    }

                    await ImportAnylandWorld(slot, area);
                });
            }

            if (notAnylandWorld.Count <= 0) return false;
            files = notAnylandWorld.ToArray();
            return true;
        }

        /// <summary>
        /// Does the import-ant stuff ;)
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="area"></param>
        /// <remarks>
        /// Presently, we skip:
        /// - inc: Name IDs
        /// - tp_XXX Physics
        /// - auto-continuation
        /// - changed vertices
        /// - body
        /// </remarks>
        private static async Task ImportAnylandWorld(Slot slot, Area area)
        {
            if (area == null) return;
            if (area.thingDefinitions == null) return;

            await default(ToWorld);
            var objectRoot = slot.AttachComponent<ObjectRoot>();
            await default(ToBackground);

            foreach (var thing in area.thingDefinitions)
            {
                if (thing == null) continue;
                if (thing.thingDescriptor == null) continue;

                await default(ToWorld);
                var child = slot.AddSlot(thing.thingDescriptor.n ?? "Thing");
                await default(ToBackground);

                child = await StateConverter.Convert(child, thing.thingDescriptor.s); // Place this first so transforms have priority
                child = await AttributeConverter.Convert(child, thing.thingDescriptor.a);
                child = await CommentConverter.Convert(child, thing.thingDescriptor.d);
                child = await TagConverter.Convert(child, thing.thingDescriptor.v.ToString());
                child = await PartConverter.Convert(child, thing.thingDescriptor.p);
            }

            // TODO: Test world optimizations
            await default(ToWorld);
            objectRoot.RemoveChildrenObjectRoots();
            MaterialOptimizer.DeduplicateMaterials(slot);
            WorldOptimizer.DeduplicateStaticProviders(slot);
            WorldOptimizer.CleanupUnreferencedAssets(slot);
            WorldOptimizer.CleanupEmptySlots(slot);
            WorldOptimizer.CleanupSlotsWithNonpersistentComponents(slot);
            await default(ToBackground);

        }
    }
}
