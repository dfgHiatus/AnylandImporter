using FrooxEngine;
using System.Threading.Tasks;

namespace AnylandImporter.Converters;

internal class TextConverter
{
    internal static async Task<Slot> Convert(Slot slot, string text, double lineHeight)
    {
        if (!string.IsNullOrEmpty(text))
        {
            await default(ToWorld);
            var tr = slot.AttachComponent<TextRenderer>();
            tr.Text.Value = text;
            tr.LineHeight.Value = (float)lineHeight;
            await default(ToBackground);
        }

        return slot;
    }
}
