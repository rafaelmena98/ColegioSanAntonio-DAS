using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColegioSanJoseMVC.Data;
using ColegioSanJoseMVC.Models;

namespace ColegioSanJoseMVC.Controllers
{
    public class ExpedienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpedienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expediente
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expediente.Include(e => e.Alumno).Include(e => e.Materia);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expediente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expediente
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // GET: Expediente/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList( _context.Alumno.Select(a => new { AlumnoId = a.AlumnoId, NombreCompleto = a.Nombre + " " + a.Apellido }),
            "AlumnoId", "NombreCompleto");
            ViewData["MateriaId"] = new SelectList( _context.Materia, "MateriaId", "NombreMateria");
            return View();
        }

        // POST: Expediente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "Apellido", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "Docente", expediente.MateriaId);
            return View(expediente);
        }

        // GET: Expediente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expediente.FindAsync(id);
            if (expediente == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno.Select(a => new { AlumnoId = a.AlumnoId, NombreCompleto = a.Nombre + " " + a.Apellido }),
             "AlumnoId", "NombreCompleto", expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList(_context.Materia, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        // POST: Expediente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpedienteId,AlumnoId,MateriaId,NotaFinal,Observaciones")] Expediente expediente)
        {
            if (id != expediente.ExpedienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpedienteExists(expediente.ExpedienteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList( _context.Alumno.Select(a => new { AlumnoId = a.AlumnoId,NombreCompleto = a.Nombre + " " + a.Apellido }),
            "AlumnoId","NombreCompleto",expediente.AlumnoId);
            ViewData["MateriaId"] = new SelectList( _context.Materia, "MateriaId", "NombreMateria", expediente.MateriaId);
            return View(expediente);
        }

        // GET: Expediente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expediente = await _context.Expediente
                .Include(e => e.Alumno)
                .Include(e => e.Materia)
                .FirstOrDefaultAsync(m => m.ExpedienteId == id);
            if (expediente == null)
            {
                return NotFound();
            }

            return View(expediente);
        }

        // POST: Expediente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expediente = await _context.Expediente.FindAsync(id);
            if (expediente != null)
            {
                _context.Expediente.Remove(expediente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpedienteExists(int id)
        {
            return _context.Expediente.Any(e => e.ExpedienteId == id);
        }
    }
}
