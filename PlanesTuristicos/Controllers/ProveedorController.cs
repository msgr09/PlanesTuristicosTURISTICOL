﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlanesTuristicos.Models;

namespace PlanesTuristicos.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly PlanesTuristicosContext _context;

        public ProveedorController(PlanesTuristicosContext context)
        {
            _context = context;
        }

        // GET: Proveedor
        public async Task<IActionResult> Index()
        {
              return View(await _context.Proveedor.ToListAsync());
        }

        // GET: Proveedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id_Proveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Proveedor,nombreProveedor,Rut,Cedula,Correo,direccion,Clave")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Proveedor,nombreProveedor,Rut,Cedula,Correo,direccion,Clave")] Proveedor proveedor)
        {
            if (id != proveedor.Id_Proveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.Id_Proveedor))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("PerfilProveedor","Inicio");
            }
            return View(proveedor);
        }

        // GET: Proveedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedor == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .FirstOrDefaultAsync(m => m.Id_Proveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedor == null)
            {
                return Problem("Entity set 'PlanesTuristicosContext.Proveedor'  is null.");
            }
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedor.Remove(proveedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("IniciarSesion2","Inicio");
        }

        private bool ProveedorExists(int id)
        {
          return _context.Proveedor.Any(e => e.Id_Proveedor == id);
        }
    }
}
