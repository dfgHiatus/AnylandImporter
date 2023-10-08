using AnylandImporter.Common;
using System.Text.Json;

namespace AnylandImporter.Merger;

public class Program
{
    private const string MergedPath = "MergedAnylandWorlds";

    public static void Main()
    {
        if (!Directory.Exists(MergedPath))
        {
            Directory.CreateDirectory(MergedPath);
            Console.WriteLine($"Created merged Anyland world directory at {MergedPath}");
        }

        while (true) 
        {
            //if (!EnsureValidFileExists("Enter the path of the placement file:", out var placementData))
            //    continue;
            //if (!EnsureValidFileExists("Enter the path of the area file:", out var areaData))
            //    continue;

            var areaData = File.ReadAllText("Tests/buildtown__57f67019817496af5268f719_rr591bbb78578375c81f557a52.json");
            var placementData = File.ReadAllText("Tests/buildtown__57f67019817496af5268f719_rr591bbb78578375c81f557a52_areaData.json");

            Console.WriteLine("Starting merge...");

            if (!DeserializeFile<Placements>(placementData, out var p))
                continue;
            if (!DeserializeFile<Area>(areaData, out var a))
                continue;

            p.area = a;

            var content = JsonSerializer.Serialize(p);
            var path = Path.Combine(MergedPath, $"{p.areaName ?? "mergedAnylandWorld"}.anyland");
            File.WriteAllText(path, content);

            Console.WriteLine("Merge Complete! File saved to " + path);
            break;
        }
    }

    private static bool EnsureValidFileExists(string prompt, out string contents)
    {
        Console.WriteLine(prompt);
        var path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine($"File {path} does not exist. Please try again");
            contents = string.Empty;
            return false;
        }

        contents = File.ReadAllText(path);
        return true;
    }

    private static bool DeserializeFile<T>(string fileContents, out T file)
    {
        try
        {
            file = JsonSerializer.Deserialize<T>(fileContents)!;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An exception occurred while deserializing. Please try again.");
            Console.WriteLine(e.Message);
            file = default!;
            return false;
        }    
    }
}