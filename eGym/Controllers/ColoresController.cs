using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;

namespace eGym.Controllers
{
    public class ColoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public ColoresController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Colores
        public async Task<IActionResult> Index()
        {
              return View(await _context.Colores.ToListAsync());
        }

        // GET: Colores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Colores == null)
            {
                return NotFound();
            }

            var color = await _context.Colores
                .FirstOrDefaultAsync(m => m.idColor == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idColor,nombre,imagenColor")] Color color)
        {
            if (ModelState.IsValid)
            {
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Colores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Colores == null)
            {
                return NotFound();
            }

            var color = await _context.Colores.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idColor,nombre,imagenColor")] Color color)
        {
            if (id != color.idColor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.idColor))
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
            return View(color);
        }

        // GET: Colores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Colores == null)
            {
                return NotFound();
            }

            var color = await _context.Colores
                .FirstOrDefaultAsync(m => m.idColor == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Colores == null)
            {
                return Problem("Entity set 'AppDbContext.Colores'  is null.");
            }
            var color = await _context.Colores.FindAsync(id);
            if (color != null)
            {
                _context.Colores.Remove(color);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(int id)
        {
          return _context.Colores.Any(e => e.idColor == id);
        }
    }
}
