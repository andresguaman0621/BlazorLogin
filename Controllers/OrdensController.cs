using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using apprueba.Data;
using apprueba.Models;

namespace apprueba.Controllers
{
    public class OrdensController : Controller
    {
        public IActionResult Iniciar()
        {
            //Borrar datos de la ase de datos
            var existingProducts = _context.Orden.ToList();
            _context.Orden.RemoveRange(existingProducts);
            _context.SaveChanges();
            
            //Crear productos de tipo producto
            var OrdenUno = new Orden { Nombre = "Alitas", Precio = 10.00, Imagen = "https://www.citycountry.net.au/wp-content/uploads/2021/09/242610-Steggles.jpg" };
            var OrdenDos = new Orden { Nombre = "Nuggets", Precio = 12.00, Imagen = "https://5.imimg.com/data5/FW/BX/MY-5028075/500gm-chicken-nuggets-250x250.jpg" };
            var OrdenTres = new Orden { Nombre = "Papas Fritas", Precio = 15.00, Imagen = "https://nourishplate.com/wp-content/uploads/2020/03/Air-fryer-french-fries-9-250x250.jpg" };
            var OrdenCuatro = new Orden { Nombre = "Hamburguesa", Precio = 13.00, Imagen = "https://media.pedilo.store/1686070724541-DSC08566-1.jpg?w=250&h=250&q=100&fm=webp" };

            //Agregar los productos a la base de datos
            _context.Orden.AddRange(OrdenUno, OrdenDos, OrdenTres, OrdenCuatro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        private readonly appruebaContext _context;

        public OrdensController(appruebaContext context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            return _context.Orden != null ?
                        View(await _context.Orden.ToListAsync()) :
                        Problem("Entity set 'appruebaContext.Orden'  is null.");
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orden == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Imagen,ConSalsa,ConEnsalada,PapasExtra")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orden == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Imagen,ConSalsa,ConEnsalada,PapasExtra")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
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
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orden == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orden == null)
            {
                return Problem("Entity set 'appruebaContext.Orden'  is null.");
            }
            var orden = await _context.Orden.FindAsync(id);
            if (orden != null)
            {
                _context.Orden.Remove(orden);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return (_context.Orden?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
