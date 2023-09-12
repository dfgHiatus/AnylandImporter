using FrooxEngine;
using System.Collections.Generic;

namespace AnylandImporter.Converters;

internal class AttributeConverter
{
    internal static void Convert(ref Slot slot, List<ThingAttribute> attributes)
    {
        if (attributes == null) return;

        foreach (var attr in attributes)
        {
            switch (attr)
            {
                case ThingAttribute.isHoldable:
                    slot.AttachComponent<Grabbable>();
                    break;
                case ThingAttribute.isClimbable:
                    slot.AttachComponent<LocomotionGrip>();
                    break;
            }
        }
    }
}
