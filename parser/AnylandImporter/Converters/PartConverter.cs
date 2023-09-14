using BaseX;
using FrooxEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class PartConverter
{
    internal static async Task<Slot> Convert(Slot slot, List<Part> parts)
    {
        if (parts == null) return slot;

        foreach (Part part in parts) 
        {
            await default(ToWorld);
            var partSlot = slot.AddSlot(part.n ?? "Part");
            await default(ToBackground);

            partSlot = await AttributeConverter.Convert(partSlot, part.a);
            partSlot = await CommentConverter.Convert(partSlot, part.n);
            partSlot = await TextConverter.Convert(partSlot, part.e, part.lh);
            partSlot = await MeshConverter.Convert(partSlot, part.b, part.t, part.im, part.t1, part.t2); // part.imt is unused - Neos doesn't care

            if (part.i != null)
            {
                foreach (var subthing in part.i)
                {
                    await default(ToWorld);
                    var subthingSlot = slot.AddSlot(subthing.n ?? "Subthing");
                    subthingSlot.LocalPosition = (float3)new double3(subthing.p[0], subthing.p[1], subthing.p[2]);
                    subthingSlot.LocalRotation = floatQ.Euler((float3)new double3(subthing.r[0], subthing.r[1], subthing.r[2]));
                    await default(ToBackground);
                }
            }

            if (part.su != null)
            {
                foreach (var subthing in part.su)
                {
                    await default(ToWorld);
                    var subthingSlot = slot.AddSlot("Placed Subthing");
                    subthingSlot.LocalPosition = (float3)new double3(subthing.p[0], subthing.p[1], subthing.p[2]);
                    subthingSlot.LocalRotation = floatQ.Euler((float3)new double3(subthing.r[0], subthing.r[1], subthing.r[2]));
                    await default(ToBackground);
                }
            }

            await ParticleConverter.Convert(partSlot, part.pr);
        }

        return slot;
    }
}
