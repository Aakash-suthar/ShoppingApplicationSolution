using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext (DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<WebApp.Models.Product> Product { get; set; }

        public DbSet<WebApp.Models.Orders> Orders { get; set; }

        public DbSet<WebApp.Models.Payment> Payment { get; set; }

        public DbSet<WebApp.Models.Shipmentagent3> Shipmentagent { get; set; }
    }
}
