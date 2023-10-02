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
                await ProcessSubthings(slot, part);
            }

            if (part.su != null)
            {
                await ProcessPlacedSubthings(slot, part);
            }

            await ParticleConverter.Convert(partSlot, part.pr);
        }

        return slot;
    }

    private static async Task ProcessSubthings(Slot slot, Part part)
    {
        foreach (var subthing in part.i)
        {
            await default(ToWorld);
            var subthingSlot = slot.AddSlot(subthing.n ?? "Subthing");
            if (Utils.TryAnylandVector3ToFloat3(subthing.p, out var position))
                subthingSlot.LocalPosition = position;
            if (Utils.TryAnylandQuaternionToFloatQ(subthing.r, out var rotation))
                subthingSlot.LocalRotation = rotation;
            await default(ToBackground);
        }
    }

    private static async Task ProcessPlacedSubthings(Slot slot, Part part)
    {
        foreach (var subthing in part.su)
        {
            await default(ToWorld);
            var subthingSlot = slot.AddSlot("Placed Subthing");
            if (Utils.TryAnylandVector3ToFloat3(subthing.p, out var position))
                subthingSlot.LocalPosition = position;
            if (Utils.TryAnylandQuaternionToFloatQ(subthing.r, out var rotation))
                subthingSlot.LocalRotation = rotation;
            await default(ToBackground);
        }
    }
}
