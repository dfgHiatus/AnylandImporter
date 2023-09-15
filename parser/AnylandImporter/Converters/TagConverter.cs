using FrooxEngine;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class TagConverter
{
    internal static async Task<Slot> Convert(Slot slot, string tag)
    {
        if (!string.IsNullOrEmpty(tag))
        {
            await default(ToWorld);
            slot.Tag = tag;
            await default(ToBackground);
        }

        return slot;
    }
}
