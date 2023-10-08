using AnylandImporter.Common;
using AnylandImporter.Converters;
using Elements.Assets;
using Elements.Core;
using FrooxEngine;
using HarmonyLib;
using Newtonsoft.Json;
using ResoniteModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityFrooxEngineRunner;

namespace AnylandImporter;

public class Importer : ResoniteMod
{
    public override string Name => "AnylandImporter";
    public override string Author => "dfgHiatus";
    public override string Version => "1.0.0";

    [AutoRegisterConfigKey]
    internal static readonly ModConfigurationKey<ColorProfile> AnylandColorProfile = 
        new("colorProfile", "The ColorProfile to import anyland colors in", () => ColorProfile.sRGB);

    public static ModConfiguration Config;
    public const string AnylandWorldExtension = ".anyland";
    internal static readonly string CachePath = Path.Combine(Engine.Current.AppPath, "rml_mods", "AnylandImporter");

    public override void OnEngineInit()
    {
        new Harmony("net.dfgHiatus.AnylandImporter").PatchAll();
        Config = GetConfiguration();
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
                    Placements placements;
                    try
                    {
                        placements = JsonConvert.DeserializeObject<Placements>(File.ReadAllText(file));
                    }
                    catch (Exception e)
                    {
                        Error($"Failed to deserialize Placements {file}: {e.Message}");
                        return;
                    }

                    if (placements == null)
                    {
                        Error($"Deserialized Placements, but file was invalid: {file}");
                        return;
                    }

                    await ImportAnylandWorld(slot, placements);
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
        /// <param name="placements"></param>
        /// <remarks>
        /// Presently, we skip:
        /// - inc: Name IDs
        /// - tp_XXX Physics
        /// - auto-continuation
        /// - changed vertices
        /// - body
        /// </remarks>
        private static async Task ImportAnylandWorld(Slot slot, Placements placements)
        {
            if (placements == null) return;

            // TODO - Map EnvironmentChangers to Resonite
            //var environmentChangers =
            //    JsonConvert.DeserializeObject<EnvironmentChanger[]>
            //    (Regex.Unescape(placements.environmentChangersJSON));

            await default(ToWorld);
            if (!string.IsNullOrEmpty(placements.areaName))
                slot.Name = placements.areaName;
            var objectRoot = slot.AttachComponent<ObjectRoot>();
            await default(ToBackground);

            // Construct a dictionary of all the things's transforms in the world
            Dictionary<string, List<AnylandTransformModel>> transformDictionary = new();
            foreach (var id in placements.area.thingDefinitions.Select(t => t.id))
            {
                var associatedTransforms = placements.placements.Where(p => id == p.Tid);
                List<AnylandTransformModel> atms = new();

                foreach (var placement in associatedTransforms)
                {
                    AnylandTransformModel atm = new AnylandTransformModel()
                    {
                        Position = new Vector3(placement.P.x, placement.P.y, placement.P.z).ToEngine(),
                        Rotation = Quaternion.Euler(placement.P.x, placement.P.y, placement.P.z).ToEngine(),
                        Scale = placement.S == 0 ? float3.One : new Vector3(placement.S, placement.S, placement.S).ToEngine()
                    };
                    atms.Add(atm);
                }

                transformDictionary.Add(id, atms);
            }
            UniLog.Log("Importing " + transformDictionary.Count + " placements");

            // We need to deserialize the environmentChangersJSON and the thingDefinitions as we go
            foreach (var thing in placements.area.thingDefinitions)
            {
                if (thing == null) continue;

                var thingDescriptor = 
                    JsonConvert.DeserializeObject<ThingDescriptor>(Regex.Unescape(thing.def));

                await default(ToWorld);
                var child = slot.AddSlot(thingDescriptor.n ?? "Thing");
                var transform = transformDictionary[thing.id].First();
                child.GlobalPosition = transform.Position;
                child.GlobalRotation = transform.Rotation;
                child.GlobalScale = transform.Scale;
                await default(ToBackground);

                child = await StateConverter.Convert(child, thingDescriptor.s); // Place this first so transforms have priority
                child = await AttributeConverter.Convert(child, thingDescriptor.a);
                child = await CommentConverter.Convert(child, thingDescriptor.d);
                child = await TagConverter.Convert(child, thingDescriptor.v.ToString());
                child = await PartConverter.Convert(child, thingDescriptor.p);

                await default(ToWorld);
                foreach (var t in transformDictionary[thing.id].Skip(1))
                {
                    // Duplicate the child for each transform
                    var dupe = child.Duplicate();
                    dupe.GlobalPosition = t.Position;
                    dupe.GlobalRotation = t.Rotation;
                    dupe.GlobalScale = t.Scale;
                }
                await default(ToBackground);
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

public class AnylandTransformModel
{
    public float3 Position;
    public floatQ Rotation;
    public float3 Scale;
}
