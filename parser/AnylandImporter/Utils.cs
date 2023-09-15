using BaseX;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityNeos;

namespace AnylandImporter;

internal class Utils
{
    internal static bool ContainsUnicodeCharacter(string input)
    {
        const int MaxAnsiCode = 255;
        return input.Any(c => c > MaxAnsiCode);
    }

    // Credit to delta for this method https://github.com/XDelta/
    internal static string GenerateMD5(string filepath)
    {
        using var hasher = MD5.Create();
        using var stream = File.OpenRead(filepath);
        var hash = hasher.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "");
    }

    internal static bool TryAnylandVector3ToFloat3(double[] arr, out float3 target)
    {
        target = float3.Zero;
        if (arr != null) return false;
        if (arr.Length >= 3) return false;
        target = new Vector3((float)arr[0], (float)arr[1], (float)arr[2]).ToNeos();
        return true;
    }

    internal static bool TryAnylandQuaternionToFloatQ(double[] arr, out floatQ target)
    {
        target = floatQ.Identity;
        if (arr != null) return false;
        if (arr.Length >= 3) return false;
        target = Quaternion.Euler((float)arr[0], (float)arr[1], (float)arr[2]).ToNeos();
        return true;
    }

    internal static bool TryAnylandColorToColor(double[] arr, out color target)
    {
        target = color.White;
        if (arr != null) return false;
        if (arr.Length >= 3) return false;
        target = new Color((float)arr[0], (float)arr[1], (float)arr[2]).ToNeos();
        return true;
    }
}
