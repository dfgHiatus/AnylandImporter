//using FrooxEngine;
//using System;
//using System.IO;
//using System.Reflection;
//using System.Runtime.InteropServices;

//namespace AnylandImporter.Resonite;

//internal static class DependencyManager
//{
//    static DependencyManager()
//    {
//        var dirName = Path.Combine(
//            Engine.Current.AppPath,
//            "rml_mods",
//            "AnylandImporter.Resonite.Dependencies");

//        if (!Directory.Exists(dirName))
//            Directory.CreateDirectory(dirName);

//        string[] deps = new string[]
//        {
//            "AnylandImporter.Common.dll"
//        };

//        foreach (var dep in deps)
//        {
//            var dllPath = Path.Combine(dirName, dep);

//            using (var stm = Assembly.
//                GetExecutingAssembly().
//                GetManifestResourceStream($"AnylandImporter.Resonite.Libs.{dep}"))
//            {
//                using (System.IO.Stream outFile = File.Create(dllPath))
//                {
//                    const int sz = 4096;
//                    var buf = new byte[sz];
//                    while (true)
//                    {
//                        if (stm == null)
//                            throw new FileNotFoundException($"[AnylandImporter.Resonite] Could not load resource {dep} from AnylandImporter.Resonite.dll");
//                        var nRead = stm.Read(buf, 0, sz);
//                        if (nRead < 1)
//                            break;
//                        outFile.Write(buf, 0, nRead);
//                    }
//                }

//                LoadLibrary(dllPath);
//            }
//        }
//    }

//    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
//    private static extern IntPtr LoadLibrary(string lpFileName);
//}
