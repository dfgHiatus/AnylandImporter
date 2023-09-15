using AnylandImporter.Assets;
using FrooxEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AnylandImporter;

internal class MeshConverter
{
    internal static async Task<Slot> Convert(Slot partSlot, ThingPartBase thingPartBase, MaterialType materialType, 
        string url, TextureType t1, TextureType t2)
    {
        if (thingPartBase == ThingPartBase.None) return partSlot;

        var anylandModelName = AnylandModelDictionary.Models[thingPartBase];
        var path = Path.Combine(AnylandModelDictionary.Path, anylandModelName);
        if (!File.Exists(path)) return partSlot; // Not all anyland meshes are available (yet!)

        // TODO: Get the slot UniversalImporter creates

        await default(ToWorld);
        await UniversalImporter.Import(path, Engine.Current.WorldManager.FocusedWorld, partSlot.GlobalPosition, partSlot.GlobalRotation, true);
        var importSlot = Engine.Current.WorldManager.FocusedWorld.RootSlot.Find(anylandModelName);
        importSlot.SetParent(partSlot); // Will preserve global transform
        importSlot.AttachComponent<MeshCollider>();
        await default(ToBackground);

        if (t1 != TextureType.None && t2 != TextureType.None)
        {
            await default(ToWorld);
            partSlot = await DetermineTexture(importSlot, new List<TextureType>() { t1, t2 });
            await default(ToBackground);
        }

        await default(ToWorld);
        StaticTexture2D t2d = null;
        if (!string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uriResult))
        {
            t2d = importSlot.AttachComponent<StaticTexture2D>();
            t2d.URL.Value = uriResult;
        }
        await default(ToBackground);

        await default(ToWorld);
        partSlot = await DetermineMaterial(importSlot, materialType, t2d, importSlot.GetComponent<ProceduralTexture>());
        await default(ToBackground);

        return partSlot;
    }

    private static async Task<Slot> DetermineMaterial(Slot partSlot, MaterialType t, StaticTexture2D t2d, ProceduralTexture pt) // TODO: Implement more materials, assign pt
    {
        var mr = partSlot.GetComponent<MeshRenderer>(); // There should always be a MeshRenderer attached to this slot
        if (mr == null) mr = partSlot.AttachComponent<MeshRenderer>();

        switch (t)
        {
            case MaterialType.Unshiny:
                var us = await SetupMetallic(partSlot, mr, t2d, pt);
                us.Metallic.Value = 0;
                us.Smoothness.Value = 0;
                break;
            case MaterialType.Metallic:
                await SetupMetallic(partSlot, mr, t2d, pt);
                break;
            case MaterialType.VeryMetallic:
                var vm = await SetupMetallic(partSlot, mr, t2d, pt);
                vm.Metallic.Value = 1;
                break;
            case MaterialType.Brightness:
            case MaterialType.Glow:
                await SetupUnlit(partSlot, mr, t2d, pt);
                break;
            default:
                await SetupMetallic(partSlot, mr, t2d, pt);
                break;
        }

        return partSlot;
    }

    private static async Task<PBS_Metallic> SetupMetallic(Slot partSlot, MeshRenderer mr, StaticTexture2D t2d, ProceduralTexture pt)
    {
        var mat = partSlot.AttachComponent<PBS_Metallic>();
        await TextureSetHelper(mat.AlbedoTexture, t2d, pt);
        if (mr.Materials.Count == 0)
            mr.Materials.Add(mat);
        else
            mr.Materials[0] = mat;
        return mat;
    }

    private static async Task<UnlitMaterial> SetupUnlit(Slot partSlot, MeshRenderer mr, StaticTexture2D t2d, ProceduralTexture pt)
    {
        var mat = partSlot.AttachComponent<UnlitMaterial>();
        await TextureSetHelper(mat.Texture, t2d, pt);
        if (mr.Materials.Count == 0)
            mr.Materials.Add(mat);
        else
            mr.Materials[0] = mat;
        return mat;
    }

    private static async Task<AssetRef<ITexture2D>> TextureSetHelper(AssetRef<ITexture2D> provider, StaticTexture2D t2d, ProceduralTexture pt)
    {
        await default(ToWorld);
        if (pt != null) provider.Target = pt;
        if (t2d != null) provider.Target = t2d; // Prioritize StaticTexture2Ds over ProceduralTextures
        await default(ToBackground);

        return provider;
    }

    private static async Task<Slot> DetermineTexture(Slot partSlot, List<TextureType> t)
    {
        if (t == null) return partSlot;

        foreach (var textureType in t)
        {
            var anylandTextureName = AnylandTextureDictionary.Textures[textureType];
            var path = Path.Combine(AnylandTextureDictionary.Path, anylandTextureName);
            if (!File.Exists(path)) continue;

            await ImageImporter.ImportImage(path, partSlot);

            //switch (texture)
            //{
            //    case TextureType.PerlinNoise1:
            //        partSlot.AttachComponent<NoiseTexture>();
            //        break;
            //}
        }

        return partSlot;
    }
}
