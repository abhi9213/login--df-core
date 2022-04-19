using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using login__df_core.Models;

#nullable disable

namespace login__df_core.datafoldr
{
    public partial class mvcdatabaseContext : DbContext
    {
        public mvcdatabaseContext()
        {
        }

        public mvcdatabaseContext(DbContextOptions<mvcdatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Emptable> Emptables { get; set; }
        public virtual DbSet<Logtable> Logtables { get; set; }
        public virtual DbSet<Newtab> Newtabs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=ADMIN-PC;database=mvcdatabase;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Emptable>(entity =>
            {
                entity.ToTable("emptable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Mobile).HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Sal).HasColumnName("sal");

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            modelBuilder.Entity<Logtable>(entity =>
            {
                entity.ToTable("logtable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Pass)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Newtab>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("newtab");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<login__df_core.Models.loginClass> loginClass { get; set; }

        public DbSet<login__df_core.Models.empClass> empClass { get; set; }
    }
}
