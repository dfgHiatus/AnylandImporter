using AnylandImporter.Converters;
using AnylandImporter.Tests;
using BaseX;
using CodeX;
using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AnylandImporter;

public class Importer : NeosMod
{
    public override string Name => "AnylandImporter";

    public override string Author => "dfgHiatus";

    public override string Version => "1.0.0";

    public static ModConfiguration config;
    public const string AnylandWorldExtension = ".anyland";
    internal static readonly string CachePath = Path.Combine(Engine.Current.AppPath, "nml_mods", "AnylandImporter", "baseShapes");

    public override void OnEngineInit()
    {
        new Harmony("net.dfgHiatus.AnylandImporter").PatchAll();
        config = GetConfiguration();
        Directory.CreateDirectory(CachePath);
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
                var area = JsonConvert.DeserializeObject<Area>(File.ReadAllText(file));
                ImportAnylandWorld(slot, area);
            }
            if (notAnylandWorld.Count <= 0) return false;
            files = notAnylandWorld.ToArray();
            return true;
        }

        /// <summary>
        /// 
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
        private static void ImportAnylandWorld(Slot slot, Area area)
        {
            if (area == null) return;
            if (area.thingDefinitions == null) return;

            foreach (var thing in area.thingDefinitions)
            {
                if (thing == null) continue;
                if (thing.thingDescriptor == null) continue;

                var child = slot.AddSlot(thing.thingDescriptor.n ?? "Thing");
                AttributeConverter.Convert(ref child, thing.thingDescriptor.a);
                CommentConverter.Convert(ref child, thing.thingDescriptor.d);
                TagConverter.Convert(ref child, thing.thingDescriptor.v.ToString());
                PartConverter.Convert(ref child, thing.thingDescriptor.p);
                StateConverter.Convert(ref child, thing.thingDescriptor.s);
            }
        }
    }
}
