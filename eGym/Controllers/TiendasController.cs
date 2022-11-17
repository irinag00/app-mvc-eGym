using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;
using Microsoft.AspNetCore.Authorization;
using eGym.ModelsView;

namespace eGym.Controllers
{
    [Authorize]
    public class TiendasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;
        public TiendasController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env; 
        }

        // GET: Tiendas
        public async Task<IActionResult> Index(string busqNombre, int pagina = 1)
        {
            paginador paginador = new paginador()
            {
                CantidadRegistros = _context.Tiendas.Count(),
                PaginaActual = pagina,
                RegistrosxPagina = 3
            };

            var consulta = _context.Tiendas.Select(a => a);
            if (!string.IsNullOrEmpty(busqNombre))
            {
                consulta = consulta.Where(e => e.nombre.Contains(busqNombre));
            }

            paginador.CantidadRegistros = consulta.Count();

            //ViewData["paginador"] = paginador;

            var datosAMostrar = consulta
                .Skip((paginador.PaginaActual - 1) * paginador.RegistrosxPagina)
                .Take(paginador.RegistrosxPagina);

            foreach (var item in Request.Query)
                paginador.ValoresQueryString.Add(item.Key, item.Value);

            TiendaViewModel Datos = new TiendaViewModel()
            {
                ListaTienda = await datosAMostrar.ToListAsync(),
                busqNombre = busqNombre,
                paginador = paginador,
            };
            return View(Datos);
            //return View(await _context.Tiendas.ToListAsync());
        }

        // GET: Tiendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tiendas == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas
                .FirstOrDefaultAsync(m => m.idTienda == id);
            if (tienda == null)
            {
                return NotFound();
            }

            return View(tienda);
        }
        [Authorize(Roles = Roles.Admin)]
        // GET: Tiendas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tiendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idTienda,nombre,resumen,imagenTienda")] Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                var archivos = HttpContext.Request.Form.Files;
                if (archivos != null && archivos.Count > 0)
                {
                    var archivoFoto = archivos[0];
                    //var pathDestino = Path.Combine(env.WebRootPath, "fotos\\portadas-libros");
                    if (archivoFoto.Length > 0)
                    {
                        var pathDestino = Path.Combine(env.WebRootPath, "pictures\\img-tienda");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            tienda.imagenTienda = archivoDestino;
                        };
                    }
                }
                _context.Add(tienda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tienda);
        }

        // GET: Tiendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tiendas == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            return View(tienda);
        }

        // POST: Tiendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idTienda,nombre,resumen,imagenTienda")] Tienda tienda)
        {
            if (id != tienda.idTienda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var archivos = HttpContext.Request.Form.Files;
                    if (archivos != null && archivos.Count > 0)
                    {
                        var archivoFoto = archivos[0];
                        var pathDestino = Path.Combine(env.WebRootPath, "pictures\\img-tienda");
                        if (archivoFoto.Length > 0)
                        {
                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(tienda.imagenTienda))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, tienda.imagenTienda);
                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                tienda.imagenTienda = archivoDestino;
                            };
                        }
                    }
                    _context.Update(tienda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiendaExists(tienda.idTienda))
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
            return View(tienda);
        }

        // GET: Tiendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tiendas == null)
            {
                return NotFound();
            }

            var tienda = await _context.Tiendas
                .FirstOrDefaultAsync(m => m.idTienda == id);
            if (tienda == null)
            {
                return NotFound();
            }

            return View(tienda);
        }

        // POST: Tiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tiendas == null)
            {
                return Problem("Entity set 'AppDbContext.Tiendas'  is null.");
            }
            var tienda = await _context.Tiendas.FindAsync(id);
            if (tienda != null)
            {
                _context.Tiendas.Remove(tienda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiendaExists(int id)
        {
          return _context.Tiendas.Any(e => e.idTienda == id);
        }
    }
}
