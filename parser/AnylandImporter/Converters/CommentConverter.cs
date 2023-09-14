using FrooxEngine;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class CommentConverter
{
    internal static async Task<Slot> Convert(Slot slot, string comment)
    {
        if (!string.IsNullOrEmpty(comment))
        {
            await default(ToWorld);
            slot.AttachComponent<Comment>().Text.Value = comment;
            await default(ToBackground);
        }

        return slot;
    }
}
