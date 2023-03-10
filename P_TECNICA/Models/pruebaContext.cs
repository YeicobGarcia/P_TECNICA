using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P_TECNICA.Models
{
    public partial class pruebaContext : DbContext
    {
        public pruebaContext()
        {
        }

        public pruebaContext(DbContextOptions<pruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //             optionsBuilder.UseMySql("server=localhost;port=3308;database=prueba;uid=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuarios)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuarios)
                    .ValueGeneratedNever()
                    .HasColumnName("idUsuarios");

                entity.Property(e => e.Email).HasMaxLength(45);

                entity.Property(e => e.Fnacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(45);

                entity.Property(e => e.Pass).HasMaxLength(45);

                entity.Property(e => e.Username).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
