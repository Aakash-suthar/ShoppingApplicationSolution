using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShipmentApi.model
{
    public partial class NewShoppingDbContext : DbContext
    {
      /*  public NewShoppingDbContext()
        {
        }
*/
        public NewShoppingDbContext(DbContextOptions<NewShoppingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Shipmentagent3> Shipmentagent3 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:newshopping.database.windows.net,1433;Initial Catalog=NewShoppingDb;Persist Security Info=False;User ID=shopadmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipmentagent3>(entity =>
            {
                entity.ToTable("shipmentagent3");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeliveryGuy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Statuss)
                    .HasColumnName("statuss")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
