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
    public class ProveedoresController : Controller
    {
        private readonly EmpresaDBContext _context;

        public ProveedoresController(EmpresaDBContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index(string textoABuscar)
        {
            var proveedores = from p in _context.Proveedores
                           select p;

            if (!String.IsNullOrEmpty(textoABuscar))
            {
                proveedores = proveedores.Where(p => p.NombreProveedor.Contains(textoABuscar));
            }

            return View(await proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NombreProveedor")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el proveedor ya está registrado
                var proveedorExistente = await _context.Proveedores
                    .Where(p => p.NombreProveedor == proveedor.NombreProveedor)
                    .FirstOrDefaultAsync();

                if (proveedorExistente != null)
                {
                    ViewBag.Error = "El proveedor ya está registrado.";
                    return View(proveedor);
                }
              
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NombreProveedor")] Proveedor proveedor)
        {
            if (id != proveedor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar si el proveedor ya está registrado
                var proveedorExistente = await _context.Proveedores
                    .Where(p => p.NombreProveedor == proveedor.NombreProveedor)
                    .FirstOrDefaultAsync();

                if (proveedorExistente != null)
                {
                    ViewBag.Error = "El proveedor ya está registrado.";
                    return View(proveedor);
                }

                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.ID))
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
            return View(proveedor);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al eliminar: Un registro de compras utiliza este registro";
                return View();
            }
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.ID == id);
        }
    }
}
