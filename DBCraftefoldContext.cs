using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Craft
{
    public partial class DBCraftefoldContext : DbContext
    {
        public DBCraftefoldContext()
        {
        }

        public DBCraftefoldContext(DbContextOptions<DBCraftefoldContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Dispatch> Dispatch { get; set; }
        public virtual DbSet<Modifications> Modifications { get; set; }
        public virtual DbSet<Ordermodifications> Ordermodifications { get; set; }
        public virtual DbSet<Orderphotos> Orderphotos { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Paintcolors> Paintcolors { get; set; }
        public virtual DbSet<Painting> Painting { get; set; }
        public virtual DbSet<Paintingproduct> Paintingproduct { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Productphotos> Productphotos { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<Stages> Stages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DBCraftefold;Username=postgres;Password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("k14");

                entity.ToTable("categories");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Nameofcategory)
                    .IsRequired()
                    .HasColumnName("nameofcategory")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => e.IdColor)
                    .HasName("k9");

                entity.ToTable("colors");

                entity.Property(e => e.IdColor).HasColumnName("id_color");

                entity.Property(e => e.Colorname)
                    .IsRequired()
                    .HasColumnName("colorname")
                    .HasMaxLength(32);

                entity.Property(e => e.Colornumber)
                    .IsRequired()
                    .HasColumnName("colornumber")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<Dispatch>(entity =>
            {
                entity.HasKey(e => new { e.IdDispatch, e.IdOrder })
                    .HasName("k2");

                entity.ToTable("dispatch");

                entity.Property(e => e.IdDispatch).HasColumnName("id_dispatch");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(128);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Tracknumber)
                    .IsRequired()
                    .HasColumnName("tracknumber")
                    .HasMaxLength(32);

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Dispatch)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c1");
            });

            modelBuilder.Entity<Modifications>(entity =>
            {
                entity.HasKey(e => e.IdModification)
                    .HasName("k5");

                entity.ToTable("modifications");

                entity.Property(e => e.IdModification).HasColumnName("id_modification");

                entity.Property(e => e.Modificationname)
                    .IsRequired()
                    .HasColumnName("modificationname")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Ordermodifications>(entity =>
            {
                entity.HasKey(e => new { e.IdModification, e.IdOrder })
                    .HasName("k4");

                entity.ToTable("ordermodifications");

                entity.Property(e => e.IdModification).HasColumnName("id_modification");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.HasOne(d => d.IdModificationNavigation)
                    .WithMany(p => p.Ordermodifications)
                    .HasForeignKey(d => d.IdModification)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c4");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Ordermodifications)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c3");
            });

            modelBuilder.Entity<Orderphotos>(entity =>
            {
                entity.HasKey(e => e.IdOrderphoto)
                    .HasName("k15");

                entity.ToTable("orderphotos");

                entity.Property(e => e.IdOrderphoto).HasColumnName("id_orderphoto");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.Orderphoto)
                    .IsRequired()
                    .HasColumnName("orderphoto")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(32);

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Orderphotos)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c16");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("k1");

                entity.ToTable("orders");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.Deadline)
                    .HasColumnName("deadline")
                    .HasColumnType("date");

                entity.Property(e => e.IdPainting).HasColumnName("id_painting");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IdShop).HasColumnName("id_shop");

                entity.Property(e => e.IdSize).HasColumnName("id_size");

                entity.Property(e => e.IdStage).HasColumnName("id_stage");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("date");

                entity.Property(e => e.Specification)
                    .HasColumnName("specification")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.IdPaintingNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdPainting)
                    .HasConstraintName("c7");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c10");

                entity.HasOne(d => d.IdShopNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdShop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c2");

                entity.HasOne(d => d.IdSizeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdSize)
                    .HasConstraintName("c6");

                entity.HasOne(d => d.IdStageNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdStage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c5");
            });

            modelBuilder.Entity<Paintcolors>(entity =>
            {
                entity.HasKey(e => new { e.IdPainting, e.IdColor })
                    .HasName("k8");

                entity.ToTable("paintcolors");

                entity.Property(e => e.IdPainting).HasColumnName("id_painting");

                entity.Property(e => e.IdColor).HasColumnName("id_color");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(32);

                entity.HasOne(d => d.IdColorNavigation)
                    .WithMany(p => p.Paintcolors)
                    .HasForeignKey(d => d.IdColor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c9");

                entity.HasOne(d => d.IdPaintingNavigation)
                    .WithMany(p => p.Paintcolors)
                    .HasForeignKey(d => d.IdPainting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c8");
            });

            modelBuilder.Entity<Painting>(entity =>
            {
                entity.HasKey(e => e.IdPainting)
                    .HasName("k7");

                entity.ToTable("painting");

                entity.Property(e => e.IdPainting).HasColumnName("id_painting");

                entity.Property(e => e.Paintingname)
                    .IsRequired()
                    .HasColumnName("paintingname")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Paintingproduct>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.IdPainting })
                    .HasName("k11");

                entity.ToTable("paintingproduct");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IdPainting).HasColumnName("id_painting");

                entity.HasOne(d => d.IdPaintingNavigation)
                    .WithMany(p => p.Paintingproduct)
                    .HasForeignKey(d => d.IdPainting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c12");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Paintingproduct)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c11");
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.HasKey(e => e.IdProductphoto)
                    .HasName("k13");

                entity.ToTable("photos");

                entity.Property(e => e.IdProductphoto).HasColumnName("id_productphoto");

                entity.Property(e => e.Productphoto)
                    .IsRequired()
                    .HasColumnName("productphoto")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Productphotos>(entity =>
            {
                entity.HasKey(e => new { e.IdProductphoto, e.IdProduct })
                    .HasName("k12");

                entity.ToTable("productphotos");

                entity.Property(e => e.IdProductphoto).HasColumnName("id_productphoto");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Productphotos)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c13");

                entity.HasOne(d => d.IdProductphotoNavigation)
                    .WithMany(p => p.Productphotos)
                    .HasForeignKey(d => d.IdProductphoto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c14");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("k10");

                entity.ToTable("products");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(128);

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("c15");
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.HasKey(e => e.IdShop)
                    .HasName("k3");

                entity.ToTable("shops");

                entity.Property(e => e.IdShop).HasColumnName("id_shop");

                entity.Property(e => e.Nameofshop)
                    .IsRequired()
                    .HasColumnName("nameofshop")
                    .HasMaxLength(32);

                entity.Property(e => e.Shopaddress)
                    .IsRequired()
                    .HasColumnName("shopaddress")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Sizes>(entity =>
            {
                entity.HasKey(e => e.IdSize)
                    .HasName("k6");

                entity.ToTable("sizes");

                entity.Property(e => e.IdSize).HasColumnName("id_size");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasColumnName("size")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<Stages>(entity =>
            {
                entity.HasKey(e => e.IdStage)
                    .HasName("k16");

                entity.ToTable("stages");

                entity.Property(e => e.IdStage).HasColumnName("id_stage");

                entity.Property(e => e.Stagename)
                    .IsRequired()
                    .HasColumnName("stagename")
                    .HasMaxLength(32);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
