using FrooxEngine;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class TagConverter
{
    internal static async Task<Slot> Convert(Slot slot, string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            await default(ToWorld);
            slot.Tag = comment;
            await default(ToBackground);
        }

        return slot;
    }
}
