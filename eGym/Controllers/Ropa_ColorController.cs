using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;
using Microsoft.AspNetCore.Authorization;

namespace eGym.Controllers
{
    public class Ropa_ColorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public Ropa_ColorController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Ropa_Color
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ropa_Colores.Include(r => r.color).Include(r => r.ropa);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ropa_Color/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ropa_Colores == null)
            {
                return NotFound();
            }

            var ropa_Color = await _context.Ropa_Colores
                .Include(r => r.color)
                .Include(r => r.ropa)
                .Include(r => r.color.imagenColor)
                .FirstOrDefaultAsync(m => m.idRopa == id);
            if (ropa_Color == null)
            {
                return NotFound();
            }

            return View(ropa_Color);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Ropa_Color/Create
        public IActionResult Create()
        {
            ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "nombre");
            ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "nombre");
            return View();
        }

        // POST: Ropa_Color/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idRopa,idColor")] Ropa_Color ropa_Color)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ropa_Color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "nombre", ropa_Color.idColor);
            ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "nombre", ropa_Color.idRopa);
            return View(ropa_Color);
        }

        // GET: Ropa_Color/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ropa_Colores == null)
            {
                return NotFound();
            }

            var ropa_Color = await _context.Ropa_Colores.FindAsync(id);
            if (ropa_Color == null)
            {
                return NotFound();
            }
            ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "nombre", ropa_Color.idColor);
            ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "nombre", ropa_Color.idRopa);
            //ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "idColor", ropa_Color.idColor);
            //ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "idRopa", ropa_Color.idRopa);
            return View(ropa_Color);
        }

        // POST: Ropa_Color/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idRopa,idColor")] Ropa_Color ropa_Color)
        {
            if (id != ropa_Color.idRopa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ropa_Color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ropa_ColorExists(ropa_Color.idRopa))
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
            ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "nombre", "imagenColor", ropa_Color.idColor.ToString());
            ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "nombre", "imagenRopa", ropa_Color.idRopa.ToString());
            //ViewData["idColor"] = new SelectList(_context.Colores, "idColor", "idColor", ropa_Color.idColor);
            //ViewData["idRopa"] = new SelectList(_context.Ropas, "idRopa", "idRopa", ropa_Color.idRopa);
            return View(ropa_Color);
        }

        // GET: Ropa_Color/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ropa_Colores == null)
            {
                return NotFound();
            }

            var ropa_Color = await _context.Ropa_Colores
                .Include(r => r.color)
                .Include(r => r.ropa)
                .FirstOrDefaultAsync(m => m.idRopa == id);
            if (ropa_Color == null)
            {
                return NotFound();
            }

            return View(ropa_Color);
        }

        // POST: Ropa_Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ropa_Colores == null)
            {
                return Problem("Entity set 'AppDbContext.Ropa_Colores'  is null.");
            }
            var ropa_Color = await _context.Ropa_Colores.FindAsync(id);
            if (ropa_Color != null)
            {
                _context.Ropa_Colores.Remove(ropa_Color);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Ropa_ColorExists(int id)
        {
          return _context.Ropa_Colores.Any(e => e.idRopa == id);
        }
    }
}
