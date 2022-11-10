﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;

namespace eGym.Controllers
{
    public class RopasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public RopasController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env; 
        }

        // GET: Ropas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ropas.Include(r => r.Categoria).Include(r => r.Marca).Include(r => r.Tienda);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ropas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ropas == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas
                .Include(r => r.Categoria)
                .Include(r => r.Marca)
                .Include(r => r.Tienda)
                .FirstOrDefaultAsync(m => m.idRopa == id);
            if (ropa == null)
            {
                return NotFound();
            }

            return View(ropa);
        }

        // GET: Ropas/Create
        public IActionResult Create()
        {
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "idCategoria", "idCategoria");
            ViewData["marcaId"] = new SelectList(_context.Marcas, "idMarca", "idMarca");
            ViewData["tiendaId"] = new SelectList(_context.Tiendas, "idTienda", "idTienda");
            return View();
        }

        // POST: Ropas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idRopa,nombre,detalles,precio,imagenRopa,marcaId,tiendaId,categoriaId")] Ropa ropa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ropa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "idCategoria", "idCategoria", ropa.categoriaId);
            ViewData["marcaId"] = new SelectList(_context.Marcas, "idMarca", "idMarca", ropa.marcaId);
            ViewData["tiendaId"] = new SelectList(_context.Tiendas, "idTienda", "idTienda", ropa.tiendaId);
            return View(ropa);
        }

        // GET: Ropas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ropas == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas.FindAsync(id);
            if (ropa == null)
            {
                return NotFound();
            }
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "idCategoria", "idCategoria", ropa.categoriaId);
            ViewData["marcaId"] = new SelectList(_context.Marcas, "idMarca", "idMarca", ropa.marcaId);
            ViewData["tiendaId"] = new SelectList(_context.Tiendas, "idTienda", "idTienda", ropa.tiendaId);
            return View(ropa);
        }

        // POST: Ropas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idRopa,nombre,detalles,precio,imagenRopa,marcaId,tiendaId,categoriaId")] Ropa ropa)
        {
            if (id != ropa.idRopa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ropa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RopaExists(ropa.idRopa))
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
            ViewData["categoriaId"] = new SelectList(_context.Categorias, "idCategoria", "idCategoria", ropa.categoriaId);
            ViewData["marcaId"] = new SelectList(_context.Marcas, "idMarca", "idMarca", ropa.marcaId);
            ViewData["tiendaId"] = new SelectList(_context.Tiendas, "idTienda", "idTienda", ropa.tiendaId);
            return View(ropa);
        }

        // GET: Ropas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ropas == null)
            {
                return NotFound();
            }

            var ropa = await _context.Ropas
                .Include(r => r.Categoria)
                .Include(r => r.Marca)
                .Include(r => r.Tienda)
                .FirstOrDefaultAsync(m => m.idRopa == id);
            if (ropa == null)
            {
                return NotFound();
            }

            return View(ropa);
        }

        // POST: Ropas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ropas == null)
            {
                return Problem("Entity set 'AppDbContext.Ropas'  is null.");
            }
            var ropa = await _context.Ropas.FindAsync(id);
            if (ropa != null)
            {
                _context.Ropas.Remove(ropa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RopaExists(int id)
        {
          return _context.Ropas.Any(e => e.idRopa == id);
        }
    }
}
