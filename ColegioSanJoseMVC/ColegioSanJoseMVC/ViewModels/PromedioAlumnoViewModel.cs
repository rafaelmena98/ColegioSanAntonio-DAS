namespace ColegioSanJoseMVC.ViewModels
{
    public class PromedioAlumnoViewModel
    {

        public int AlumnoId { get; set; }
        public string NombreCompleto { get; set; }
        public string Grado { get; set; }
        public int TotalMaterias { get; set; }
        public decimal PromedioNotas { get; set; }

    }
}
