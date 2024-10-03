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
        public async Task<IActionResult> Index(string fechaInicioTexto, string fechaFinTexto)
        {
            var compras = from p in _context.Compras
                            .Include(p => p.Producto)
                            .Include(c => c.Producto.Categoria)
                            .Include(c => c.Proveedor)
                            select p;

            DateTime fechaInicio;
            DateTime fechaFin;

            bool fechaInicioValida = DateTime.TryParse(fechaInicioTexto, out fechaInicio);
            bool fechaFinValida = DateTime.TryParse(fechaFinTexto, out fechaFin);

            if (fechaInicioValida && fechaFinValida)
            {
                compras = compras.Where(p => p.FechaCompra >= fechaInicio && p.FechaCompra <= fechaFin);
            }
            else if (fechaInicioValida)
            {
                compras = compras.Where(p => p.FechaCompra >= fechaInicio);
            }
            else if (fechaFinValida)
            {
                compras = compras.Where(p => p.FechaCompra <= fechaFin);
            }

            return View(await compras.ToListAsync());

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
                .Include(c => c.Producto.Categoria)
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
            //Registra la compra
            ModelState.Remove("Proveedor");
            ModelState.Remove("Producto");
            ModelState.Remove("Categoria");
            ModelState.Remove("UnidadesCompra");

            if (ModelState.IsValid)
            { 
                if (producto.NombreProducto == null || producto.NombreProducto.ToString() == "")
                {
                    _context.Add(compra);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Verificar si el producto ya existe con todos los atributos
                    var existingProduct = _context.Productos
                        .FirstOrDefault(p => p.NombreProducto == producto.NombreProducto &&
                                             p.IdCategoria == producto.IdCategoria &&
                                             p.DescripcionProducto == producto.DescripcionProducto &&
                                             p.CostoProducto == producto.CostoProducto);

                    if (existingProduct != null)
                    {
                        // Si el producto ya existe, sumar las unidades
                        existingProduct.UnidadesProducto += producto.UnidadesProducto;
                        _context.Update(existingProduct);

                        // Actualizar la compra con el ID del producto existente
                        compra.IdProducto = existingProduct.ID;
                        compra.UnidadesCompra = producto.UnidadesProducto;
                    }
                    else
                    {
                        // Si el producto no existe, lo agrega como nuevo
                        _context.Add(producto);
                        await _context.SaveChangesAsync();

                        compra.IdProducto = producto.ID;
                        compra.UnidadesCompra = producto.UnidadesProducto;
                    }

                    _context.Add(compra);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", compra.IdProducto);
                ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria", producto.IdCategoria);

                return View(compra);
            }

        }


        //Get original
        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id,
            [Bind("ID,IdCategoria,NombreProducto,DescripcionProducto,UnidadesProducto,CostoProducto")] Producto producto)
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

            // Obtener todos los productos con sus categorías y descripciones
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new
                {
                    p.ID,
                    NombreCompleto = p.Categoria.NombreCategoria + " - " + p.NombreProducto + " - " + p.DescripcionProducto + " - $" + p.CostoProducto
                })
                .ToListAsync();

            ViewData["IdProducto"] = new SelectList(productos, "ID", "NombreCompleto", compra.IdProducto);

            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);

            return View(compra);
        }




        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,IdCategoria,NombreProducto,DescripcionProducto,UnidadesProducto,CostoProducto")] Producto producto,
            [Bind("ID,FechaCompra,IdProveedor,IdProducto,UnidadesCompra")] Compra compra)
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
                    //En vista, Se almacena el id del producto en NombreProducto
                    compra.IdProducto = Convert.ToInt32(producto.NombreProducto);

                    _context.Update(compra);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
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
            }

            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", compra.IdProducto);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria", producto.IdCategoria);

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
                .Include(c => c.Producto.Categoria)
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
            if (compra == null)
            {
                return NotFound();
            }
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.ID == id);
        }
    }
}
