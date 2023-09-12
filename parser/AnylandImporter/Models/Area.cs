using System.Text;

namespace AnylandImporter.Tests;

internal class Area
{
    public Thing[]? thingDefinitions { get; set; }
    public int serveTime { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if (thingDefinitions != null)
        {
            foreach (var thing in thingDefinitions)
            {
                sb.AppendLine(thing.ToString());
            }
        }
        sb.AppendLine($"ServeTime: {serveTime}");
        return sb.ToString();
    }
}
