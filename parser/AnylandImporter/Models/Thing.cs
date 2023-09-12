using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace AnylandImporter;

public class Thing
{
    public string id { get; set; }
    public ThingDescriptor? thingDescriptor { get; set; }

    public Thing(string id, string def)
    {
        this.id = id;

        try
        {
            thingDescriptor = JsonConvert.DeserializeObject<ThingDescriptor>(Regex.Unescape(def));
        }
        catch (JsonReaderException) { } // Malformed anyland information, skip
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"ThingDescriptor: {thingDescriptor?.ToString()}");
        sb.AppendLine($"ID: {id}");
        return sb.ToString();
    }
}