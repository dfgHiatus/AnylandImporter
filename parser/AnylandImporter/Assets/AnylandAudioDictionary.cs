using System.Collections.Generic;
using System.IO;

namespace AnylandImporter.Assets;

internal static class AnylandAudioDictionary
{
    internal static readonly string Path = System.IO.Path.Combine(Importer.CachePath, "baseAudio");


    internal static Dictionary<string, string> Sounds = new()
    {
       
    };
}
