using System.ComponentModel.DataAnnotations;

namespace ColegioSanJoseMVC.Models
{
    public class Alumno
    {

        public int AlumnoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength (100)]
        public string Apellido { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(50)]
        public string Grado { get; set; }

        public ICollection<Expediente>? Expedientes { get; set; }
    }
}
