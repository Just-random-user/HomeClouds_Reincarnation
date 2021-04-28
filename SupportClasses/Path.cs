using System.Collections.Generic;
using System.Linq;

namespace Clouds.SupportClasses
{
    static public class Path
    {
        static public List<string> GetPaths(string path)
        {
            List<string> Paths = new List<string>();
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
            return Paths;
        }
    }
}
