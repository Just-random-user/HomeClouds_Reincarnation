using System;
using System.IO;
using System.Linq;
using Clouds.Data;
using Clouds.Models;

namespace Clouds.SupportClasses
{
    public static class LinkGenerator
    {
        public static string GetDownloadLink(string login, string path, CloudsDbContext db)
        {
            string link = null;
            
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return link;
            }
            
            link = db.SharedFiles.FirstOrDefault(f => f.FilePath == path)?.Link;
            
            if (link == null)
            {
                link = Guid.NewGuid().ToString();
                var sharedFile = new SharedFiles()
                {
                    FilePath = path,
                    Link = link
                };
                var user = db.Users.FirstOrDefault(u => u.Login == login);
                if (user != null)
                {
                    sharedFile.User = user;
                    user.SharedFiles.Add(sharedFile);
                    db.Users.Update(user);
                    db.SaveChanges(); 
                }
            }

            return link;
        }
    }
}