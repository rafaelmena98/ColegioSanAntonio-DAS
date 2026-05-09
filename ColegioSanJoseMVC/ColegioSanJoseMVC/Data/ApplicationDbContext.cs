using ColegioSanJoseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioSanJoseMVC.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Expediente> Expediente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expediente>()
                .HasIndex(e => new { e.AlumnoId, e.MateriaId })
                .IsUnique();

            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Alumno)
                .WithMany(a => a.Expedientes)
                .HasForeignKey(e => e.AlumnoId);

            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Materia)
                .WithMany(m => m.Expedientes)
                .HasForeignKey(e => e.MateriaId);
        }

    }
}
