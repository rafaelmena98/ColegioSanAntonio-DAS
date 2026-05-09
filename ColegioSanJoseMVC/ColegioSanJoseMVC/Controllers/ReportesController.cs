using ColegioSanJoseMVC.Data;
using ColegioSanJoseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColegioSanJoseMVC.Controllers
{
    public class ReportesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Promedios()
        {
            var promedios = await _context.Expediente
                .Include(e => e.Alumno)
                .Where(e => e.NotaFinal != null)
                .GroupBy(e => new
                {
                    e.AlumnoId,
                    e.Alumno.Nombre,
                    e.Alumno.Apellido,
                    e.Alumno.Grado
                })
                .Select(g => new PromedioAlumnoViewModel
                {
                    AlumnoId = g.Key.AlumnoId,
                    NombreCompleto = g.Key.Nombre + " " + g.Key.Apellido,
                    Grado = g.Key.Grado,
                    TotalMaterias = g.Count(),
                    PromedioNotas = Math.Round(g.Average(x => x.NotaFinal ?? 0), 2)
                })
                .OrderBy(x => x.NombreCompleto)
                .ToListAsync();

            return View(promedios);
        }

    }
}
