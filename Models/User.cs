using System.Collections.Generic;

namespace Clouds.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public ICollection<SharedFiles> SharedFiles { get; set; } = new List<SharedFiles>();
    }
}
