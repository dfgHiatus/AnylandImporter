using FrooxEngine;

namespace AnylandImporter.Converters;

internal class TagConverter
{
    internal static void Convert(ref Slot slot, string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            slot.Tag = comment;
        }
    }
}
