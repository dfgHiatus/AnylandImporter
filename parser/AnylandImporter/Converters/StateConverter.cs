using AnylandImporter.Common;
using FrooxEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class StateConverter
{
    internal static async Task<Slot> Convert(Slot child, List<State> s)
    {
        if (s == null) return child;
        var firstState = s.FirstOrDefault();
        if (firstState == null) return child;

        await default(ToWorld);
        if (Utils.TryAnylandVector3ToFloat3(firstState.p, out var pos))
            child.LocalPosition = pos;
        if (Utils.TryAnylandQuaternionToFloatQ(firstState.r, out var rot))
            child.LocalRotation = rot;
        if (Utils.TryAnylandVector3ToFloat3(firstState.s, out var scale))
            child.LocalScale = scale;
        if (Utils.TryAnylandColorToColorX(firstState.c, out var color))
        {
            var pbs = child.GetComponent<PBS_Metallic>();
            if (pbs != null)
            {
                pbs.AlbedoColor.Value = color;
            }

            var unlit = child.GetComponent<UnlitMaterial>();
            if (unlit != null)
            {
                unlit.TintColor.Value = color;
            }
        }
        await default(ToBackground);

        return child;
    }
}
