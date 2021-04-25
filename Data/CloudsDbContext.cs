using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthTest.Models;

namespace AuthTest.Data
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
