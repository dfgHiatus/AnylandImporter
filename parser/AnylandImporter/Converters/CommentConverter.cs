using FrooxEngine;

namespace AnylandImporter.Converters;

internal class CommentConverter
{
    internal static void Convert(ref Slot slot, string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            slot.AttachComponent<Comment>().Text.Value = comment;
        }
    }
}
