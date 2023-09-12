using BaseX;
using FrooxEngine;
using System.Collections.Generic;

namespace AnylandImporter.Converters;

internal class PartConverter
{
    internal static void Convert(ref Slot slot, List<Part> parts)
    {
        if (parts == null) return;

        foreach (Part part in parts) 
        {
            var partSlot = slot.AddSlot(part.n ?? "Part");
            AttributeConverter.Convert(ref partSlot, part.a);
            CommentConverter.Convert(ref partSlot, part.n);
            TextConverter.Convert(ref partSlot, part.e, part.lh);
            MeshConverter.Convert(ref partSlot, part.b, part.t, part.im, part.t1, part.t2); // part.imt is unused - Neos doesn't care

            if (part.i != null)
            {
                foreach (var subthing in part.i)
                {
                    var subthingSlot = slot.AddSlot(subthing.n ?? "Subthing");
                    subthingSlot.LocalPosition = (float3)new double3(subthing.p[0], subthing.p[1], subthing.p[2]);
                    subthingSlot.LocalRotation = floatQ.Euler((float3)new double3(subthing.r[0], subthing.r[1], subthing.r[2]));
                }
            }

            if (part.su != null)
            {
                foreach (var subthing in part.su)
                {
                    var subthingSlot = slot.AddSlot("Placed Subthing");
                    subthingSlot.LocalPosition = (float3)new double3(subthing.p[0], subthing.p[1], subthing.p[2]);
                    subthingSlot.LocalRotation = floatQ.Euler((float3)new double3(subthing.r[0], subthing.r[1], subthing.r[2]));
                }
            }

            ParticleConverter.Convert(ref partSlot, part.pr);
        }
    }
}
