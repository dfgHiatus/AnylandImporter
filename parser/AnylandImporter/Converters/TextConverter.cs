using FrooxEngine;

namespace AnylandImporter.Converters;

internal class TextConverter
{
    internal static void Convert(ref Slot slot, string text, double lineHeight)
    {
        if (!string.IsNullOrEmpty(text))
        {
            var tr = slot.AttachComponent<TextRenderer>();
            tr.Text.Value = text;
            tr.LineHeight.Value = (float)lineHeight;
        }
    }
}
