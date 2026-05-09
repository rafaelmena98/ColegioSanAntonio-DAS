using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJoseMVC.Models
{
    public class Expediente
    {
        public int ExpedienteId { get; set; }

        [Required]
        public int AlumnoId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Range(0, 10)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? NotaFinal { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }

        public Alumno? Alumno { get; set; }

        public Materia? Materia { get; set; }

    }
}
