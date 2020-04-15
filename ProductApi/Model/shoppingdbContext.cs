using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductApi.model
{
    public partial class shoppingdbContext : DbContext
    {
       /* public shoppingdbContext()
        {
        }*/

        public shoppingdbContext(DbContextOptions<shoppingdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=shoppingdb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Imgurl)
                    .HasColumnName("imgurl")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Productdesciption)
                    .HasColumnName("productdesciption")
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Productname)
                    .HasColumnName("productname")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
