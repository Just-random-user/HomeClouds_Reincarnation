using System;
using System.Collections.Generic;
using System.Linq;

namespace Clouds.SupportClasses
{
    public static class Path
    {
        public static List<string> GetPaths(string path)
        {                
            List<string> Paths = new List<string>();
            try
            {
                if (path != null && System.IO.Directory.Exists(path))
                {
                    Paths = new List<string>(System.IO.Directory.GetDirectories(path));
                    Paths.AddRange(new List<string>(System.IO.Directory.GetFiles(path)));
                }
                else
                {
                    foreach (var d in System.IO.DriveInfo.GetDrives())
                    {
                        Paths.Add(d.Name);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return GetPaths(System.IO.Path.GetDirectoryName(path));
            }
            return Paths;
        }
    }
}
