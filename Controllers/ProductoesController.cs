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
    public class ProductoesController : Controller
    {
        private readonly appruebaContext _context;

        public ProductoesController(appruebaContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
              return _context.Producto != null ? 
                          View(await _context.Producto.ToListAsync()) :
                          Problem("Entity set 'appruebaContext.Producto'  is null.");
                          
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }




        public IActionResult InicializarProductos()
        {
            //Borrar datos de la ase de datos
            var existingProducts = _context.Producto.ToList();
            
                _context.Producto.RemoveRange(existingProducts);
                _context.SaveChanges();
            
            

            //Crear productos de tipo producto
            var productoUno = new Producto { Nombre = "Alitas", Precio = 10, Imagen = "https://www.citycountry.net.au/wp-content/uploads/2021/09/242610-Steggles.jpg" };
            var productoDos = new Producto { Nombre = "Nuggets", Precio = 12, Imagen = "https://5.imimg.com/data5/FW/BX/MY-5028075/500gm-chicken-nuggets-250x250.jpg" };
            var productoTres = new Producto { Nombre = "Papas Fritas", Precio = 15, Imagen = "https://nourishplate.com/wp-content/uploads/2020/03/Air-fryer-french-fries-9-250x250.jpg" };
            var productoCuatro = new Producto { Nombre = "Hamburguesa", Precio = 13, Imagen = "https://media.pedilo.store/1686070724541-DSC08566-1.jpg?w=250&h=250&q=100&fm=webp" };

            //Agregar los productos a la base de datos
            _context.Producto.AddRange(productoUno, productoDos, productoTres, productoCuatro);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Imagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Imagen")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'appruebaContext.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
