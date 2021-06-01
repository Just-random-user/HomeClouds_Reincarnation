using System;
using System.IO;
using System.Linq;
using Clouds.Data;
using Clouds.Models;

namespace Clouds.SupportClasses
{
    public static class LinkGenerator
    {
        public static string GetDownloadLink(int userId,string path, CloudsDbContext db)
        {
            string link = null;
            
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return link;
            }

            if (db.Users.Any(u => u.Id == userId && u.SharedFiles.Any(f => f.FilePath == path)))
            {
                link = db.SharedFiles.FirstOrDefault(f => f.FilePath == path)?.Link;
            }

            if (link == null)
            {
                link = Guid.NewGuid().ToString();
                var sharedFile = new SharedFiles()
                {
                    FilePath = path,
                    Link = link
                };
                db.SharedFiles.Add(sharedFile);
                db.Users.FirstOrDefault(u => u.Id == userId)?.SharedFiles.Add(sharedFile);
                db.SaveChanges();
            }

            return link;
        }
    }
}