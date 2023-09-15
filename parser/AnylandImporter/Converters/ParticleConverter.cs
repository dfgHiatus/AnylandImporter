using AnylandImporter.Assets;
using FrooxEngine;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AnylandImporter.Converters
{
    internal class ParticleConverter
    {
        internal static async Task<Slot> Convert(Slot partSlot, ParticleSystemType particleSystemType)
        {
            if (particleSystemType == ParticleSystemType.None) return partSlot;

            await default(ToWorld);
            var mat = partSlot.AttachComponent<UnlitMaterial>();
            mat.BlendMode.Value = BlendMode.Cutout; // Style used for most particles in Anyland
            var system = partSlot.AttachComponent<ParticleSystem>();
            var style = partSlot.AttachComponent<ParticleStyle>();
            var emitter = partSlot.AttachComponent<SphereEmitter>();
            style.Material.Target = mat;
            system.Style.Target = style;
            emitter.System.Target = system;
            await default(ToBackground);

            var anylandTextureName = AnylandParticleDictionary.Particles[particleSystemType];
            var path = Path.Combine(AnylandParticleDictionary.Path, anylandTextureName);
            if (!File.Exists(path))
            {
                Uri localUri = await Engine.Current.LocalDB.ImportLocalAssetAsync(path, LocalDB.ImportLocation.Copy)
                    .ConfigureAwait(continueOnCapturedContext: false);
                await default(ToWorld);
                var staticTexture2D = partSlot.AttachComponent<StaticTexture2D>();
                staticTexture2D.URL.Value = localUri;
                mat.Texture.Target = staticTexture2D;
                await default(ToBackground);
            }

            return partSlot;
        }
    }
}
