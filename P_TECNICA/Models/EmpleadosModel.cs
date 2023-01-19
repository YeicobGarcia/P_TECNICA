using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace P_TECNICA.Models
{
    public partial class EmpleadosModel : DbContext
    {
        public EmpleadosModel()
        {
        }

        public EmpleadosModel(DbContextOptions<EmpleadosModel> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; } = null!;

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

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.idEmpleados)
                    .HasName("PRIMARY");

                entity.ToTable("empleados");

                entity.Property(e => e.idEmpleados)
                    .ValueGeneratedNever()
                    .HasColumnName("idEmpleados");

                entity.Property(e => e.NombreE).HasMaxLength(45);

                entity.Property(e => e.DPI).HasMaxLength(45);

                entity.Property(e => e.Hijos).HasMaxLength(45);

                entity.Property(e => e.SalarioB).HasMaxLength(45);

                entity.Property(e => e.userUdpdate).HasMaxLength(45);

                entity.Property(e => e.fechaUpdate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
