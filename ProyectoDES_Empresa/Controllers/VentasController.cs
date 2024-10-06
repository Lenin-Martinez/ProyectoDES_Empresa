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
    public class VentasController : Controller
    {
        private readonly EmpresaDBContext _context;

        public VentasController(EmpresaDBContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index(string fechaInicioTexto, string fechaFinTexto, string categoriaABuscar, string productoABuscar, string descripcionABuscar)
        {

            var ventas = from p in _context.Ventas
                .Include(v => v.Empleado)
                .Include(v => v.Producto)
                .Include(v => v.Producto.Categoria)
                select p;

            if (!String.IsNullOrEmpty(fechaInicioTexto) && DateTime.TryParse(fechaInicioTexto, out DateTime fechaInicio))
            {
                ventas = ventas.Where(c => c.FechaVenta >= fechaInicio);
            }

            if (!String.IsNullOrEmpty(fechaFinTexto) && DateTime.TryParse(fechaFinTexto, out DateTime fechaFin))
            {
                ventas = ventas.Where(c => c.FechaVenta <= fechaFin);
            }

            if (!String.IsNullOrEmpty(categoriaABuscar))
            {
                ventas = ventas.Where(c => c.Producto.Categoria.NombreCategoria.Contains(categoriaABuscar));
            }

            if (!String.IsNullOrEmpty(productoABuscar))
            {
                ventas = ventas.Where(c => c.Producto.NombreProducto.Contains(productoABuscar));
            }

            if (!String.IsNullOrEmpty(descripcionABuscar))
            {
                ventas = ventas.Where(c => c.Producto.DescripcionProducto.Contains(descripcionABuscar));
            }

            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Empleado)
                .Include(v => v.Producto)
                .Include(v => v.Producto.Categoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria");
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto");
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado");

            return View();
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


        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            string CategoriaValor,
            string NombreProductoValor,
            string DescripcionProductoValor,
            [Bind("ID,FechaVenta,IdProducto,UnidadesVenta,PrecioUnitarioVenta,PrecioTotalVenta,IdEmpleado")] Venta venta)
        {
            ModelState.Remove("Empleado");
            ModelState.Remove("Producto");


            if (ModelState.IsValid)
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
                                        p.DescripcionProducto == DescripcionProductoValor);

                if (existingProduct == null)
                {
                    return NotFound();
                }
                else
                {
                    if (existingProduct != null && venta.UnidadesVenta <= existingProduct.UnidadesProducto)
                    {
                        if (venta.PrecioUnitarioVenta >= existingProduct.CostoProducto)
                        {
                            //Restar las unidades vendidas de las unidades existentes
                            existingProduct.UnidadesProducto -= venta.UnidadesVenta;
                            _context.Update(existingProduct);

                            // Agregar la venta

                            venta.IdProducto = existingProduct.ID;
                            _context.Add(venta);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "No se puede registrar la venta: El precio de venta es menor que el costo del producto";
                        }

                    }
                    else
                    {
                        ViewBag.ErrorUnidades = "No se puede registrar la venta: No hay suficientes unidades del producto.";
                    }
                }
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria");
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto");
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado");

            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(venta.IdProducto);
            if (producto == null)
            {
                return NotFound();
            }

            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria", producto.IdCategoria);
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", venta.IdProducto);
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto", venta.IdProducto);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado", venta.IdProducto);

            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            string CategoriaValor,
            string NombreProductoValor,
            string DescripcionProductoValor,
            [Bind("ID,FechaVenta,IdProducto,UnidadesVenta,PrecioUnitarioVenta,PrecioTotalVenta,IdEmpleado")] Venta venta)
        {
            ModelState.Remove("Empleado");
            ModelState.Remove("Producto");

            if (id != venta.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                                        p.DescripcionProducto == DescripcionProductoValor);

                if (existingProduct == null)
                {
                    return NotFound();
                }
                else
                {
                    if (existingProduct != null && venta.PrecioUnitarioVenta >= existingProduct.CostoProducto)
                    {
                        try
                        {
                            venta.IdProducto = existingProduct.ID;
                            _context.Update(venta);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!VentaExists(venta.ID))
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
                    else
                    {
                        ViewBag.ErrorMessage = "No se puede actualizar la venta: El precio de venta es menor que el costo del producto";
                    }
                }
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "ID", "NombreCategoria");
            ViewData["NombreProducto"] = new SelectList(_context.Productos, "NombreProducto", "NombreProducto", venta.IdProducto);
            ViewData["DescripcionProducto"] = new SelectList(_context.Productos, "DescripcionProducto", "DescripcionProducto", venta.IdProducto);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado", venta.IdProducto);

            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Empleado)
                .Include(v => v.Producto)
                .Include(v => v.Producto.Categoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();

            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.ID == id);
        }
    }
}
