using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarios.Tests
{
    public class VentasControllerTests
    {
        // Crear una venta cuando la información es válida
        [Fact]
        public async Task Crear_Venta_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 200,
                IdEmpleado = 1
            };

            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            var result = await controller.Create(Categoria,
                                                NombreProducto,
                                                DescripcionProducto, 
                                                venta);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var ventaCreada = await context.Ventas.FirstOrDefaultAsync(v => v.ID == venta.ID);
            Assert.NotNull(ventaCreada);
            Assert.Equal(5, ventaCreada.UnidadesVenta);

            var productoActualizado = await context.Productos.FirstOrDefaultAsync(p => p.ID == producto.ID);
            Assert.NotNull(productoActualizado);
            Assert.Equal(5, productoActualizado.UnidadesProducto);
        }

        // No crear una venta cuando el modelo no es válido
        [Fact]
        public async Task Crear_Venta_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 200,
                IdEmpleado = 1
            };

            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;


            // Error de validación en el PrecioUnitarioVenta
            controller.ModelState.AddModelError("PrecioUnitarioVenta", "El precio unitario es requerido.");

            var result = await controller.Create(Categoria,
                                                NombreProducto,
                                                DescripcionProducto, 
                                                venta);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.PrecioUnitarioVenta, returnValue.PrecioUnitarioVenta);
        }

        // No crear una venta cuando no hay suficientes unidades del producto
        [Fact]
        public async Task Crear_Venta_CuandoNoHaySuficientesUnidades()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 200,
                IdEmpleado = 1
            };

            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            // Error de validación en el PrecioUnitarioVenta
            controller.ModelState.AddModelError("UnidadesVenta", "No se puede registrar la venta: No hay suficientes unidades del producto.");

            var result = await controller.Create(Categoria,
                                                NombreProducto,
                                                DescripcionProducto, 
                                                venta);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.UnidadesVenta, returnValue.UnidadesVenta);
        }

        // No crear una venta cuando el precio de venta es menor que el costo del producto
        [Fact]
        public async Task Crear_Venta_CuandoPrecioVentaEsMenorQueCosto()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 1,
                PrecioTotalVenta = 5,
                IdEmpleado = 1
            };

            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            var result = await controller.Create(Categoria,
                                                NombreProducto,
                                                DescripcionProducto,
                                                venta);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.PrecioUnitarioVenta, returnValue.PrecioUnitarioVenta);
            Assert.Equal("No se puede registrar la venta: El precio de venta es menor que el costo del producto", controller.ViewBag.ErrorMessage);
        }

        // Editar una venta cuando la información es válida
        [Fact]
        public async Task Editar_Venta_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 1000,
                IdEmpleado = 1
            };

            context.Ventas.Add(venta);
            await context.SaveChangesAsync();

            // Actualiza la venta
            venta.UnidadesVenta = 3;
            venta.PrecioTotalVenta = 180;


            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            var result = await controller.Edit(venta.ID,
                                                Categoria,
                                                NombreProducto,
                                                DescripcionProducto,
                                                venta);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var ventaActualizada = await context.Ventas.FirstOrDefaultAsync(v => v.ID == venta.ID);
            Assert.NotNull(ventaActualizada);
            Assert.Equal(3, ventaActualizada.UnidadesVenta);
            Assert.Equal(180, ventaActualizada.PrecioTotalVenta);
        }

        // No editar una venta cuando el ID no es valido
        [Fact]
        public async Task Editar_Venta_CuandoIdNoValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 1000,
                IdEmpleado = 1
            };


            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            var result = await controller.Edit(2,
                                                Categoria,
                                                NombreProducto,
                                                DescripcionProducto,
                                                venta);

            Assert.IsType<NotFoundResult>(result);
        }

        // No editar una venta cuando el modelo no es válido
        [Fact]
        public async Task Editar_Venta_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 1000,
                IdEmpleado = 1
            };


            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            // Error de validación en el PrecioUnitarioVenta
            controller.ModelState.AddModelError("PrecioUnitarioVenta", "El precio unitario es requerido.");

            var result = await controller.Edit(venta.ID,
                                                Categoria,
                                                NombreProducto,
                                                DescripcionProducto,
                                                venta);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.PrecioUnitarioVenta, returnValue.PrecioUnitarioVenta);
        }

        // No editar una venta cuando el precio de venta es menor que el costo del producto
        [Fact]
        public async Task Editar_Venta_CuandoPrecioVentaEsMenorQueCosto()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea categoria asociado a la venta
            var categoria = new Categoria
            {
                NombreCategoria = "Mueble",
                DescripcionCategoria = "Muebles de prueba"
            };

            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = categoria.ID,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            // Genera la nueva venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                UnidadesVenta = 5,
                PrecioUnitarioVenta = 200,
                PrecioTotalVenta = 1000,
                IdEmpleado = 1
            };

            context.Ventas.Add(venta);
            await context.SaveChangesAsync();

            // Actualiza la venta
            venta.PrecioUnitarioVenta = 40;


            string Categoria = categoria.NombreCategoria;
            string NombreProducto = producto.NombreProducto;
            string DescripcionProducto = producto.DescripcionProducto;

            var result = await controller.Edit(venta.ID,
                                                Categoria,
                                                NombreProducto,
                                                DescripcionProducto,
                                                venta);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.PrecioUnitarioVenta, returnValue.PrecioUnitarioVenta);
            Assert.Equal("No se puede actualizar la venta: El precio de venta es menor que el costo del producto", controller.ViewBag.ErrorMessage);
        }

        // Obtener detalles de la venta cuando el ID es válido
        [Fact]
        public async Task Details_Venta_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };
            context.Productos.Add(producto);

            var empleado = new Empleado
            {
                NombreEmpleado = "Juan",
                ApellidoEmpleado = "Perez",
                ComisionVentaEmpleado = 2
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            // Crea venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                IdProducto = producto.ID,
                UnidadesVenta = 2,
                PrecioUnitarioVenta = 150,
                PrecioTotalVenta = 300,
                IdEmpleado = 1
            };
            context.Ventas.Add(venta);
            await context.SaveChangesAsync();

            // Verifica que el ID exista
            var result = await controller.Details(venta.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Venta>(viewResult.Model);
            Assert.Equal(venta.ID, returnValue.ID);
            Assert.Equal("Closet", returnValue.Producto.NombreProducto);
            Assert.Equal("Juan", returnValue.Empleado.NombreEmpleado);
        }

        // No obtiene los detalles del registro cuando el ID es inválido
        [Fact]
        public async Task Details_Venta_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        // Elimina el registro cuando el ID es válido
        [Fact]
        public async Task Eliminar_Venta_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            // Crea producto asociado a la venta
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Gavetero",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 100
            };
            context.Productos.Add(producto);

            var empleado = new Empleado
            {
                NombreEmpleado = "Manuel",
                ApellidoEmpleado = "Perez",
                ComisionVentaEmpleado = 2
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            // Crea venta
            var venta = new Venta
            {
                FechaVenta = DateTime.Now,
                IdProducto = producto.ID,
                UnidadesVenta = 2,
                PrecioUnitarioVenta = 150,
                PrecioTotalVenta = 300,
                IdEmpleado = empleado.ID
            };
            context.Ventas.Add(venta);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(venta.ID);

            // Verificar que la venta ha sido eliminada
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var ventaEnDb = context.Ventas.FirstOrDefault(v => v.ID == venta.ID);
            Assert.Null(ventaEnDb);
        }

        // No elimina el registro cuando el ID es inválido
        [Fact]
        public async Task Eliminar_Venta_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new VentasController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
