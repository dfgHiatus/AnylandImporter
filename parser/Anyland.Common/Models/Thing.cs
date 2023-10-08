using System.Text;

namespace AnylandImporter.Common;

public class Thing
{
    public string id { get; set; }
    public string def { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ThingDescriptor: {def?.ToString()}");
        sb.AppendLine($"ID: {id}");
        return sb.ToString();
    }
}