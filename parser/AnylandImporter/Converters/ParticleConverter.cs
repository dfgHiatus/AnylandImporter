using FrooxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnylandImporter.Converters
{
    internal class ParticleConverter
    {
        internal static async Task<Slot> Convert(Slot partSlot, ParticleSystemType p)
        {
            if (p == ParticleSystemType.None) return partSlot;

            await default(ToWorld);
            var system = partSlot.AttachComponent<ParticleSystem>();
            var style = partSlot.AttachComponent<ParticleStyle>();
            var emitter = partSlot.AttachComponent<SphereEmitter>();
            system.Style.Target = style;
            emitter.System.Target = system;
            await default(ToBackground);

            switch (p)
            {
                // TODO: Define particle configuration
            }

            return partSlot;
        }
    }
}
