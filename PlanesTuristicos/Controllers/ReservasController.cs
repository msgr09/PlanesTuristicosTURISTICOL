using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;

namespace PlanesTuristicos.Controllers
{
    public class ReservasController : Controller
    {
        private readonly PlanesTuristicosContext _context;

        public ReservasController(PlanesTuristicosContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var planesTuristicosContext = _context.Reserva.Include(r => r.PlanTuristico);
            return View(await planesTuristicosContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.PlanTuristico)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["IdPlanTuristico"] = new SelectList(_context.PlanesT, "Id_PlanTuristicos", "Id_PlanTuristicos");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,IdPlanTuristico,NombrePlanTuristico,Informacion,Actividades,Municipio,Imagen,Precio,NombreUsuario,CorreoUsuario,TelefonoUsuario,FechaReserva")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPlanTuristico"] = new SelectList(_context.PlanesT, "Id_PlanTuristicos", "Id_PlanTuristicos", reserva.IdPlanTuristico);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdPlanTuristico"] = new SelectList(_context.PlanesT, "Id_PlanTuristicos", "Id_PlanTuristicos", reserva.IdPlanTuristico);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,IdPlanTuristico,NombrePlanTuristico,Informacion,Actividades,Municipio,Imagen,Precio,NombreUsuario,CorreoUsuario,TelefonoUsuario,FechaReserva")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Perfil","Inicio");
            }
            ViewData["IdPlanTuristico"] = new SelectList(_context.PlanesT, "Id_PlanTuristicos", "Id_PlanTuristicos", reserva.IdPlanTuristico);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reserva == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.PlanTuristico)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reserva == null)
            {
                return Problem("Entity set 'PlanesTuristicosContext.Reserva'  is null.");
            }
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva != null)
            {
                _context.Reserva.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Perfil", "Inicio");
        }

        private bool ReservaExists(int id)
        {
          return _context.Reserva.Any(e => e.IdReserva == id);
        }
    }
}
