using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clouds.Models
{
    public class SharedFiles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Link { get; set; }

        public string FilePath { get; set; }

        public User User { get; set; }
    }
}