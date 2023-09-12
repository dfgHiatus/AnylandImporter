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
        internal static void Convert(ref Slot partSlot, ParticleSystemType p)
        {
            if (p == ParticleSystemType.None) return;

            var system = partSlot.AttachComponent<ParticleSystem>();
            var style = partSlot.AttachComponent<ParticleStyle>();
            var emitter = partSlot.AttachComponent<SphereEmitter>();
            system.Style.Target = style;
            emitter.System.Target = system;

            switch (p)
            {
                // TODO: Define particle configuration
            }
        }
    }
}
