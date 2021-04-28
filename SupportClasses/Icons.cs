using System.IO;
using System.Collections.Generic;
using System;


#nullable enable

namespace Clouds.SupportClasses
{

    static public class Icons
    {
        static readonly string iconsPath = $"{System.IO.Path.DirectorySeparatorChar}icons{System.IO.Path.DirectorySeparatorChar}";
        static public string IconPath(string filePath, string rootPath)
        {
            if (new List<DriveInfo>(DriveInfo.GetDrives()).Find(d => d.Name == filePath) != null)
            {
                return $"{iconsPath}disc.svg";
            }

            string? ext = System.IO.Path.GetExtension(filePath);
            if (Directory.Exists(filePath))
            {
                return $"{iconsPath}folder.svg";
            }

            List<string?> types = new List<string?>();
            foreach (var n in Directory.GetFiles($"{rootPath}{System.IO.Path.DirectorySeparatorChar}icons"))
            {
                types.Add(System.IO.Path.GetFileNameWithoutExtension(n));
            }

            return $"{iconsPath}{types.Find(t => t == ext) ?? "default"}.svg";
        }

    }
}
