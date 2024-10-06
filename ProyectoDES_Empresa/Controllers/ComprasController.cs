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
        public async Task<IActionResult> Index(string fechaInicioTexto, string fechaFinTexto, string categoriaABuscar, string productoABuscar, string descripcionABuscar)
        {
            var compras = from p in _context.Compras
                            .Include(p => p.Producto)
                            .Include(c => c.Producto.Categoria)
                            .Include(c => c.Proveedor)
                            select p;

            if (!String.IsNullOrEmpty(fechaInicioTexto) && DateTime.TryParse(fechaInicioTexto, out DateTime fechaInicio))
            {
                compras = compras.Where(c => c.FechaCompra >= fechaInicio);
            }

            if (!String.IsNullOrEmpty(fechaFinTexto) && DateTime.TryParse(fechaFinTexto, out DateTime fechaFin))
            {
                compras = compras.Where(c => c.FechaCompra <= fechaFin);
            }

            if (!String.IsNullOrEmpty(categoriaABuscar))
            {
                compras = compras.Where(c => c.Producto.Categoria.NombreCategoria.Contains(categoriaABuscar));
            }

            if (!String.IsNullOrEmpty(productoABuscar))
            {
                compras = compras.Where(c => c.Producto.NombreProducto.Contains(productoABuscar));
            }

            if (!String.IsNullOrEmpty(descripcionABuscar))
            {
                compras = compras.Where(c => c.Producto.DescripcionProducto.Contains(descripcionABuscar));
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


        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound();
            }

            //Sector de compras
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compras.IdProveedor);


            //Sector de producto
            var prod = await _context.Productos.FindAsync(compras.IdProducto);
            if (prod == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria", prod.IdCategoria);

            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", prod.NombreProducto);
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto", compras.Producto.DescripcionProducto);
            ViewData["CostoProducto"] = new SelectList(_context.Productos, "CostoProducto", "CostoProducto", compras.Producto.CostoProducto);

            return View(compras);
        }

        //Funciones necesaria para filtrar de manera descendente

        public JsonResult GetProductosByCategoria(int id)
        {
            var productos = _context.Productos.Where(p => p.IdCategoria == id).Select(p => new
            {
                id = p.ID,
                nombre = p.NombreProducto,
                descripcion = p.DescripcionProducto,
                costo = p.CostoProducto
            }).ToList();

            return Json(productos);
        }

        public JsonResult GetProductoDetalles(int id)
        {
            var producto = _context.Productos
                .Where(p => p.ID == id)
                .Select(p => new
                {
                    id = p.ID,
                    descripcion = p.DescripcionProducto,
                    costo = p.CostoProducto
                })
                .FirstOrDefault();

            return Json(producto);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            string CategoriaValor,
            string NombreProductoValor,
            string DescripcionProductoValor,
            string CostoProductoValor,
            [Bind("ID,FechaCompra,IdProveedor,IdProducto,UnidadesCompra")] Compra compra)
        {
            ModelState.Remove("Proveedor");
            ModelState.Remove("Producto");
            ModelState.Remove("Categoria");
            ModelState.Remove("UnidadesProducto");

            if (id != compra.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar si el producto ya existe con los atributos
                    var existingCategory = _context.Categorias
                        .FirstOrDefault(p => p.NombreCategoria == CategoriaValor);

                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    var existingProduct = _context.Productos
                        .FirstOrDefault(p =>
                                            p.IdCategoria == existingCategory.ID &&
                                            p.NombreProducto == NombreProductoValor &&
                                            p.DescripcionProducto == DescripcionProductoValor &&
                                            p.CostoProducto == Convert.ToDecimal(CostoProductoValor));

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        compra.IdProducto = existingProduct.ID;

                        _context.Update(compra);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }

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

            //Sector de compras
            ViewData["IdProveedor"] = new SelectList(_context.Proveedores, "ID", "NombreProveedor", compra.IdProveedor);

            //Sector de producto
            var prod = await _context.Productos.FindAsync(compra.IdProducto);
            if (prod == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria", prod.IdCategoria);

            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", prod.NombreProducto);
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto", compra.Producto.DescripcionProducto);
            ViewData["CostoProducto"] = new SelectList(_context.Productos, "CostoProducto", "CostoProducto", compra.Producto.CostoProducto);

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
