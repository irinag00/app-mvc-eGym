using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;
using eGym.ModelsView;
using Microsoft.AspNetCore.Authorization;

namespace eGym.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public CategoriasController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env; 
        }

        public IActionResult Importar()
        {
            var archivos = HttpContext.Request.Form.Files;
            if (archivos != null && archivos.Count > 0)
            {
                var archivoImpo = archivos[0];
                var pathDestino = Path.Combine(env.WebRootPath, "importaciones");
                if (archivoImpo.Length > 0)
                {
                    var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoImpo.FileName);
                    string rutaCompleta = Path.Combine(pathDestino, archivoDestino);
                    using (var filestream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        archivoImpo.CopyTo(filestream);
                    };

                    using (var file = new FileStream(rutaCompleta, FileMode.Open))
                    {
                        List<string> renglones = new List<string>();
                        List<Categoria> CategoriaArch = new List<Categoria>();

                        StreamReader fileContent = new StreamReader(file); // System.Text.Encoding.Default
                        do
                        {
                            renglones.Add(fileContent.ReadLine());
                        }
                        while (!fileContent.EndOfStream);

                        foreach (string renglon in renglones)
                        {
                            int salida;
                            string[] datos = renglon.Split(';');
                            {
                                Categoria categoriaImportada = new Categoria()
                                {
                                    nombre = datos[0]
                                };
                                CategoriaArch.Add(categoriaImportada);
                            }
                        }
                        if (CategoriaArch.Count > 0)
                        {
                            _context.Categorias.AddRange(CategoriaArch);
                            _context.SaveChanges();
                        }

                        ViewBag.cantReng = CategoriaArch.Count + " de " + renglones.Count;
                    }
                }
            }

            return View();
        }

        // GET: Categorias
        public async Task<IActionResult> Index(string busqNombre,int pagina = 1)
        {
            paginador paginador = new paginador()
            {
                CantidadRegistros = _context.Categorias.Count(),
                PaginaActual = pagina,
                RegistrosxPagina = 3
            };

            var consulta = _context.Categorias.Select(a => a);
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

            CategoriaViewModel Datos = new CategoriaViewModel()
            {
                ListaCategoria = await datosAMostrar.ToListAsync(),
                busqNombre = busqNombre,
                paginador = paginador,
            };
            return View(Datos);

            //return View(await datosAmostrar.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.idCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCategoria,nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCategoria,nombre")] Categoria categoria)
        {
            if (id != categoria.idCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.idCategoria))
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.idCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'AppDbContext.Categorias'  is null.");
            }
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
          return _context.Categorias.Any(e => e.idCategoria == id);
        }
    }
}
