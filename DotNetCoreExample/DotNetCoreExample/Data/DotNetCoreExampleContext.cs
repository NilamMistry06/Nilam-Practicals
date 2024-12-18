using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreExample.Models;

namespace DotNetCoreExample.Data
{
    public class DotNetCoreExampleContext : DbContext
    {
        public DotNetCoreExampleContext (DbContextOptions<DotNetCoreExampleContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetCoreExample.Models.Movie> Movie { get; set; } = default!;
    }
}
