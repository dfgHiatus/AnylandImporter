using AnylandImporter.Assets;
using BaseX;
using FrooxEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;

namespace AnylandImporter;

internal class MeshConverter
{
    internal static void Convert(ref Slot partSlot, ThingPartBase thingPartBase, MaterialType materialType, 
        string url, TextureType t1, TextureType t2)
    {
        if (thingPartBase == ThingPartBase.None) return;

        var path = Path.Combine(Importer.CachePath, AnylandModelDictionary.Models[thingPartBase]);
        if (!File.Exists(path)) return; // Not all anyland meshes are available (yet!)

        var partChild = partSlot.AddSlot("Part Child"); // Cannot pass a ref into an async delegate
        Engine.Current.WorldManager.FocusedWorld.RootSlot.StartGlobalTask(async delegate
        {
            await ModelImporter.ImportModelAsync(path, partChild, new ModelImportSettings()); // FIXME
        });

        partSlot.AttachComponent<MeshCollider>();

        if (t1 != TextureType.None && t2 != TextureType.None)
        {
            DetermineTexture(ref partSlot, new List<TextureType>() { t1, t2 });
        }

        StaticTexture2D t2d = null;
        if (!string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult))
        {
            t2d = partSlot.AttachComponent<StaticTexture2D>();
            t2d.URL.Value = uriResult;
        }

        DetermineMaterial(ref partSlot, materialType, t2d, partSlot.GetComponent<ProceduralTexture>());
    }

    private static void DetermineMaterial(ref Slot partSlot, MaterialType t, StaticTexture2D t2d, ProceduralTexture pt) // TODO: Implement more materials, assign pt
    {
        var mr = partSlot.GetComponent<MeshRenderer>(); // There should always be a MeshRenderer attached to this slot
        if (mr == null) mr = partSlot.AttachComponent<MeshRenderer>();

        switch (t)
        {
            case MaterialType.Unshiny:
                var us = SetupMetallic(ref partSlot, ref mr, ref t2d, ref pt);
                us.Metallic.Value = 0;
                us.Smoothness.Value = 0;
                break;
            case MaterialType.Metallic:
                SetupMetallic(ref partSlot, ref mr, ref t2d, ref pt);
                break;
            case MaterialType.VeryMetallic:
                var vm = SetupMetallic(ref partSlot, ref mr, ref t2d, ref pt);
                vm.Metallic.Value = 1;
                break;
            case MaterialType.Brightness:
            case MaterialType.Glow:
                SetupUnlit(ref partSlot, ref mr, ref t2d, ref pt);
                break;
            default:
                SetupMetallic(ref partSlot, ref mr, ref t2d, ref pt);
                break;
        }
    }

    private static PBS_Metallic SetupMetallic(ref Slot partSlot, ref MeshRenderer mr, ref StaticTexture2D t2d, ref ProceduralTexture pt)
    {
        var mat = partSlot.AttachComponent<PBS_Metallic>();
        TextureSetHelper(mat.AlbedoTexture, ref t2d, ref pt);
        mr.Materials.Add(mat);
        return mat;
    }

    private static UnlitMaterial SetupUnlit(ref Slot partSlot, ref MeshRenderer mr, ref StaticTexture2D t2d, ref ProceduralTexture pt)
    {
        var mat = partSlot.AttachComponent<UnlitMaterial>();
        TextureSetHelper(mat.Texture, ref t2d, ref pt);
        mr.Materials.Add(mat);
        return mat;
    }

    private static void TextureSetHelper(AssetRef<ITexture2D> provider, ref StaticTexture2D t2d, ref ProceduralTexture pt)
    {
        if (pt != null) provider.Target = pt;
        if (t2d != null) provider.Target = t2d; // Prioritize StaticTexture2Ds over ProceduralTextures
    }

    private static void DetermineTexture(ref Slot partSlot, List<TextureType> t)
    {
        if (t == null) return;

        foreach (var texture in t)
        {
            switch (texture)
            {
                case TextureType.PerlinNoise1:
                    partSlot.AttachComponent<NoiseTexture>();
                    break;
            }
        }
    }
}
