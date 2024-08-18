using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.Models;

namespace ProyectoDES_Empresa.Controllers
{
    public class VentasController : Controller
    {
        private readonly EmpresaDBContext _context;

        public VentasController(EmpresaDBContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var empresaDBContext = _context.Ventas.Include(v => v.Empleado).Include(v => v.Producto);
            return View(await empresaDBContext.ToListAsync());
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FechaVenta,IdProducto,UnidadesVenta,PrecioUnitarioVenta,PrecioTotalVenta,IdEmpleado")] Venta venta)
        {
            ModelState.Remove("Empleado");
            ModelState.Remove("Producto");

            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado", venta.IdEmpleado);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", venta.IdProducto);
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado", venta.IdEmpleado);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", venta.IdProducto);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FechaVenta,IdProducto,UnidadesVenta,PrecioUnitarioVenta,PrecioTotalVenta,IdEmpleado")] Venta venta)
        {
            ModelState.Remove("Empleado");
            ModelState.Remove("Producto");

            if (id != venta.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "ID", "NombreEmpleado", venta.IdEmpleado);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "ID", "NombreProducto", venta.IdProducto);
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
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.ID == id);
        }
    }
}
