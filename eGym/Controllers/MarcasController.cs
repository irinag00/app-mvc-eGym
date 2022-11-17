using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eGym.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using eGym.ModelsView;

namespace eGym.Controllers
{
    [Authorize]
    public class MarcasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment env;

        public MarcasController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        //public IActionResult Importar()
        //{
        //    var archivos = HttpContext.Request.Form.Files;
        //    if (archivos != null && archivos.Count > 0)
        //    {
        //        var archivoImpo = archivos[0];
        //        var pathDestino = Path.Combine(env.WebRootPath, "importaciones");
        //        if (archivoImpo.Length > 0)
        //        {
        //            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoImpo.FileName);
        //            string rutaCompleta = Path.Combine(pathDestino, archivoDestino);
        //            using (var filestream = new FileStream(rutaCompleta, FileMode.Create))
        //            {
        //                archivoImpo.CopyTo(filestream);
        //            };

        //            using (var file = new FileStream(rutaCompleta, FileMode.Open))
        //            {
        //                List<string> renglones = new List<string>();
        //                List<Marca> MarcaArch = new List<Marca>();

        //                StreamReader fileContent = new StreamReader(file); // System.Text.Encoding.Default
        //                do
        //                {
        //                    renglones.Add(fileContent.ReadLine());
        //                }
        //                while (!fileContent.EndOfStream);

        //                foreach (string renglon in renglones)
        //                {
        //                    int salida;
        //                    string[] datos = renglon.Split(';');
        //                    int ropa = int.TryParse(datos[datos.Length - 1], out salida) ? salida : 0;
        //                    if (ropa > 0 && _context.Ropas.Where(c => c.idRopa == ropa).FirstOrDefault() != null)
        //                    {
        //                        Marca alumnotemporal = new Marca()
        //                        {
                                    
        //                            nombre = datos[0],
        //                            resumen= datos[1],

        //                            //inscripcion = int.TryParse(datos[1], out salida) ? salida : 0,
        //                            //estado = datos[2] == "1" ? true : false,
        //                            //fechaNac = DateTime.TryParse(datos[3], out DateTime fecha) ? fecha : DateTime.MinValue
        //                        };
        //                        MarcaArch.Add(alumnotemporal);
        //                    }
        //                }
        //                if (MarcaArch.Count > 0)
        //                {
        //                    _context.Marcas.AddRange(MarcaArch);
        //                    _context.SaveChanges();
        //                }

        //                ViewBag.cantReng = MarcaArch.Count + " de " + renglones.Count;
        //            }
        //        }
        //    }

        //    return View();
        //}

        // GET: Marcas
        public async Task<IActionResult> Index(string busqNombre, int pagina = 1)
        {
            paginador paginador = new paginador()
            {
                CantidadRegistros = _context.Marcas.Count(),
                PaginaActual = pagina,
                RegistrosxPagina = 3
            };

            var consulta = _context.Marcas.Select(a => a);
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

            MarcaViewModel Datos = new MarcaViewModel()
            {
                ListaMarca = await datosAMostrar.ToListAsync(),
                busqNombre = busqNombre,
                paginador = paginador,
            };
            return View(Datos);
            //return View(await _context.Marcas.ToListAsync());
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.idMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMarca,nombre,resumen,imagenMarca")] Marca marca)
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
                        var pathDestino = Path.Combine(env.WebRootPath, "pictures\\img-marca");
                        var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                        {
                            archivoFoto.CopyTo(filestream);
                            marca.imagenMarca = archivoDestino;
                        };
                    }
                }
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idMarca,nombre,resumen,imagenMarca")] Marca marca)
        {
            if (id != marca.idMarca)
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
                        var pathDestino = Path.Combine(env.WebRootPath, "pictures\\img-marca");
                        if (archivoFoto.Length > 0)
                        {
                            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

                            if (!string.IsNullOrEmpty(marca.imagenMarca))
                            {
                                string fotoAnterior = Path.Combine(pathDestino, marca.imagenMarca);
                                if (System.IO.File.Exists(fotoAnterior))
                                    System.IO.File.Delete(fotoAnterior);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestino, archivoDestino), FileMode.Create))
                            {
                                archivoFoto.CopyTo(filestream);
                                marca.imagenMarca = archivoDestino;
                            };
                        }
                    }
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.idMarca))
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
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.idMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marcas == null)
            {
                return Problem("Entity set 'AppDbContext.Marcas'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
          return _context.Marcas.Any(e => e.idMarca == id);
        }
    }
}
