using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System;

namespace AnylandImporter.Tests;

internal class Program
{
    static void Main(string[] args)
    {
        // DeleteEmptyWorlds("areas");
        // TestThing("zetaphor's lab_test.json");
        ConvertWorlds("areasConverted");
    }

    private static void DeleteEmptyWorlds(string directoryPath)
    {
        try
        {
            // Use EnumerateFiles to efficiently enumerate files in the directory
            foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.json", SearchOption.AllDirectories))
            {
                // Process each JSON file as needed
                var area = JsonConvert.DeserializeObject<Area>(File.ReadAllText(filePath));
                if (area.thingDefinitions.Length == 0)
                {
                    File.Delete(filePath);
                }
            }
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    private static void TestThing(string json)
    {
        var area = JsonConvert.DeserializeObject<Area>(File.ReadAllText(json));
        Console.WriteLine(area!.ToString());
    }

    private static void ConvertWorlds(string directoryPath)
    {
        try
        {
            var files = Directory.EnumerateFiles(directoryPath, "*.json", SearchOption.AllDirectories);

            // Use Parallel.ForEach to process files in parallel
            Parallel.ForEach(files, filePath =>
            {
                var area = JsonConvert.DeserializeObject<Area>(File.ReadAllText(filePath));
                var converted = JsonConvert.SerializeObject(area, Formatting.Indented);
                File.WriteAllText($"{filePath}_converted.json", converted);
            });
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
}