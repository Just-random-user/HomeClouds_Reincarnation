using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clouds.Models;

namespace Clouds.Data
{
    public class CloudsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public CloudsDbContext(DbContextOptions<CloudsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
