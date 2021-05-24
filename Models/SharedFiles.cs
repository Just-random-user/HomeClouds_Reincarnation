using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clouds.Models
{
    public class SharedFiles
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string FilePath { get; set; }
        
    }
}