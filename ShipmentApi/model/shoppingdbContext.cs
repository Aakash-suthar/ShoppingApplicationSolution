using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShipmentApi.model
{
    public partial class shoppingdbContext : DbContext
    {
       /* public shoppingdbContext()
        {
        }
*/
        public shoppingdbContext(DbContextOptions<shoppingdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Shipmentagent> Shipmentagent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=shoppingdb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipmentagent>(entity =>
            {
                entity.ToTable("shipmentagent");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeliveryGuy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Deliverydate)
                    .HasColumnName("deliverydate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Orderplacedate)
                    .HasColumnName("orderplacedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

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
