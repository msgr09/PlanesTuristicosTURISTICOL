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
    public class PlanesTsController : Controller
    {
        private readonly PlanesTuristicosContext _context;

        public PlanesTsController(PlanesTuristicosContext context)
        {
            _context = context;
        }

        // GET: PlanesTs
        public async Task<IActionResult> Index()
        {
            var planesTuristicosContext = _context.PlanesT.Include(p => p.Proveedor);
            return View(await planesTuristicosContext.ToListAsync());
        }

        // GET: PlanesTs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlanesT == null)
            {
                return NotFound();
            }

            var planesT = await _context.PlanesT
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id_PlanTuristicos == id);
            if (planesT == null)
            {
                return NotFound();
            }

            return View(planesT);
        }

        // GET: PlanesTs/Create
        public IActionResult Create()
        {
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id_Proveedor", "Id_Proveedor");
            return View();
        }

        // POST: PlanesTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_PlanTuristicos,Nombre_PlanTuristico,Rut,Municipio,Precio,Actividades,Duracion,Informacion,Imagen,IdProveedor")] PlanesT planesT)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planesT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id_Proveedor", "Id_Proveedor", planesT.IdProveedor);
            return View(planesT);
        }

        // GET: PlanesTs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlanesT == null)
            {
                return NotFound();
            }

            var planesT = await _context.PlanesT.FindAsync(id);
            if (planesT == null)
            {
                return NotFound();
            }
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id_Proveedor", "Id_Proveedor", planesT.IdProveedor);
            return View(planesT);
        }

        // POST: PlanesTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_PlanTuristicos,Nombre_PlanTuristico,Rut,Municipio,Precio,Actividades,Duracion,Informacion,Imagen,IdProveedor")] PlanesT planesT)
        {
            if (id != planesT.Id_PlanTuristicos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planesT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanesTExists(planesT.Id_PlanTuristicos))
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
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "Id_Proveedor", "Id_Proveedor", planesT.IdProveedor);
            return View(planesT);
        }

        // GET: PlanesTs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlanesT == null)
            {
                return NotFound();
            }

            var planesT = await _context.PlanesT
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(m => m.Id_PlanTuristicos == id);
            if (planesT == null)
            {
                return NotFound();
            }

            return View(planesT);
        }

        // POST: PlanesTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlanesT == null)
            {
                return Problem("Entity set 'PlanesTuristicosContext.PlanesT'  is null.");
            }
            var planesT = await _context.PlanesT.FindAsync(id);
            if (planesT != null)
            {
                _context.PlanesT.Remove(planesT);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanesTExists(int id)
        {
          return _context.PlanesT.Any(e => e.Id_PlanTuristicos == id);
        }
    }
}
