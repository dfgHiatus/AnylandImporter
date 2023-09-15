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
                case ThingAttribute.invisible:
                    ModifyExistingComponent<MeshRenderer>(slot, false, out var m);
                    break;
                case ThingAttribute.isNeverClonable:
                    slot.AttachComponent<DuplicateBlock>();
                    break;
                // case ThingAttribute.isUnwalkable:
                // case ThingAttribute.isPassable:
                case ThingAttribute.uncollidable:
                    ModifyExistingComponent<Collider>(slot, false, out var c);
                    break;
                case ThingAttribute.avoidCastShadow:
                    SetupShadows(slot, ShadowCastMode.Off);
                    break;
                case ThingAttribute.avoidReceiveShadow:
                    SetupShadows(slot, ShadowCastMode.ShadowOnly);
                    break;
                case ThingAttribute.isHoldable:
                    var grab = slot.AttachComponent<Grabbable>();
                    grab.EnabledField.Value = true;
                    grab.Scalable.Value = true;
                    break;
                case ThingAttribute.isClimbable:
                    var grip = slot.AttachComponent<LocomotionGrip>();
                    grip.EnabledField.Value = true;
                    break;
                case ThingAttribute.currentlyUnused1:
                case ThingAttribute.hideEffectShapes_deprecated:
                default:
                    break;
            }
            await default(ToBackground);
        }

        return slot;
    }

    private static bool ModifyExistingComponent<T>(Slot slot, bool enabled, out T component) where T : Component
    {
        component = null;
        var c = slot.GetComponent<T>();
        if (c != null)
        {
            c.EnabledField.Value = enabled;
            component = c;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the shadow cast mode of the mesh renderer. This runs on the main thread.
    /// </summary>
    /// <param name="hasShadows"></param>
    private static void SetupShadows(Slot slot, ShadowCastMode shadowCastMode)
    {
        var mr = slot.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.ShadowCastMode.Value = shadowCastMode;
        }
    }
}
