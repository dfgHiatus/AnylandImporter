using FrooxEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class AttributeConverter
{
    internal static async Task<Slot> Convert(Slot slot, List<ThingAttribute> attributes)
    {
        if (attributes == null) return slot;

        foreach (var attr in attributes)
        {
            await default(ToWorld);
            switch (attr)
            {
                case ThingAttribute.isHoldable:
                    slot.AttachComponent<Grabbable>();
                    break;
                case ThingAttribute.isClimbable:
                    slot.AttachComponent<LocomotionGrip>();
                    break;
            }
            await default(ToBackground);
        }

        return slot;
    }
}
