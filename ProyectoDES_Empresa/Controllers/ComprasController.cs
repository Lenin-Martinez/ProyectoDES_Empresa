using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.Models;

namespace ProyectoDES_Empresa.Controllers
{
    [Authorize]
    public class ComprasController : Controller
    {
        private readonly EmpresaDBContext _context;

        public ComprasController(EmpresaDBContext context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var empresaDBContext = _context.Compras.Include(c => c.Producto).Include(c => c.Proveedor);
            return View(await empresaDBContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Producto)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor");

            //Carga los datos de los formularios parciales agregados
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,IdCategoria,NombreProducto,DescripcionProducto,UnidadesProducto,CostoProducto")] Producto producto,
            [Bind("ID,FechaCompra,IdProveedor,IdProducto,UnidadesCompra")] Compra compra)
        {
            ModelState.Remove("Proveedor");
            ModelState.Remove("Producto");

            if (producto.NombreProducto == null || producto.NombreProducto.ToString() == "")
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();

                compra.IdProducto = producto.ID;
                compra.UnidadesCompra = producto.UnidadesProducto;

                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", compra.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FechaCompra,IdProveedor,IdProducto,UnidadesCompra")] Compra compra)
        {
            ModelState.Remove("Proveedor");
            ModelState.Remove("Producto");

            if (id != compra.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.ID))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", compra.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Producto)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.ID == id);
        }
    }
}
