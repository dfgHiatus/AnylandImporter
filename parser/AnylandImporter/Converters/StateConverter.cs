using BaseX;
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
        if (firstState.p != null)
            if (firstState.p.Length >= 3)
            child.LocalPosition = (float3) new double3(firstState.p[0], firstState.p[1], firstState.p[2]);
        if (firstState.r != null)
            if (firstState.r.Length >= 3)
                child.LocalRotation = floatQ.Euler((float3) new double3(firstState.r[0], firstState.r[1], firstState.r[2]));
        if (firstState.s != null)
            if (firstState.s.Length >= 3)
                child.LocalScale = (float3)new double3(firstState.s[0], firstState.s[1], firstState.s[2]);
        await default(ToBackground);

        return child;
    }
}
