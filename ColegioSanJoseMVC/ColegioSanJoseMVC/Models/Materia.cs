using System.ComponentModel.DataAnnotations;

namespace ColegioSanJoseMVC.Models
{
    public class Materia
    {
        public int MateriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreMateria { get; set; }

        [Required]
        [StringLength(100)]
        public string Docente { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}
