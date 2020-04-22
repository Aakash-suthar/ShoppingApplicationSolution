using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShipmentApp.Models;

namespace ShipmentApp.Data
{
    public class ShipmentAppContext : DbContext
    {
        public ShipmentAppContext (DbContextOptions<ShipmentAppContext> options)
            : base(options)
        {
        }

        public DbSet<ShipmentApp.Models.Shipmentagent> Shipmentagent { get; set; }
    }
}
