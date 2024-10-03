using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarios.Tests
{
    public class ComprasControllerTests
    {
        // Crea una nueva compra y un nuevo producto cuando el producto no existe
        [Fact]
        public async Task Crear_Compra_ProductoNuevo_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            //Crea un nuevo producto
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            //Crea la nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = 1,
                UnidadesCompra = 10
            };

            //Crea ambos registros en sus respectivos modelos
            var result = await controller.Create(producto, compra);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var productoCreado = await context.Productos.FirstOrDefaultAsync(p => p.NombreProducto == "Pintura gris");
            Assert.NotNull(productoCreado);
            Assert.Equal(10, productoCreado.UnidadesProducto);

            var compraCreada = await context.Compras.FirstOrDefaultAsync(c => c.IdProducto == productoCreado.ID);
            Assert.NotNull(compraCreada);
            Assert.Equal(10, compraCreada.UnidadesCompra);
        }

        // Actualiza las unidades del producto existente y crea una nueva compra
        [Fact]
        public async Task Crear_Compra_ProductoExistente_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            //Crea un nuevo producto inicial
            var productoExistente = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 5,
                CostoProducto = 100
            };
            context.Productos.Add(productoExistente);
            await context.SaveChangesAsync();

            //Establece el producto asociado a la compra
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 10,
                CostoProducto = 100
            };

            //Genera la nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = 1,
                UnidadesCompra = 10
            };

            var result = await controller.Create(producto, compra);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Verifica la suma de las unidades existentes con las ingresadas
            var productoActualizado = await context.Productos.FirstOrDefaultAsync(p => p.NombreProducto == "Pintura gris");
            Assert.NotNull(productoActualizado);
            Assert.Equal(15, productoActualizado.UnidadesProducto);

            var compraCreada = await context.Compras.FirstOrDefaultAsync(c => c.IdProducto == productoActualizado.ID);
            Assert.NotNull(compraCreada);
            Assert.Equal(10, compraCreada.UnidadesCompra);
        }

        // No crea la compra cuando el modelo no es válido
        [Fact]
        public async Task Crear_Compra_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            //Crea un nuevo producto
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 10,
                CostoProducto = 100
            };
            //Crea una nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = 1,
                UnidadesCompra = 10
            };

            //Se envia un error de NombreProducto null
            controller.ModelState.AddModelError("NombreProducto", "El nombre es requerido.");

            var result = await controller.Create(producto, compra);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Compra>(viewResult.Model);
            Assert.Equal(compra, returnValue);
        }

        //Edita cuando la informacion es valida
        [Fact]
        public async Task Editar_Compra_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            //Establece el producto asociado a la compra
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 5,
                CostoProducto = 100
            };

            //Genera la nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = 1,
                UnidadesCompra = 5
            };

            //Actualizamos el parametro
            producto.UnidadesProducto = 10;

            var result = await controller.Create(producto, compra);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Se comprueba que el dato ha sido actualizado
            var compraActualizada = await context.Compras.FirstOrDefaultAsync(c => c.ID == compra.ID);
            Assert.NotNull(compraActualizada);
            Assert.Equal(10, compraActualizada.UnidadesCompra);
        }

        //No Edita cuando la informacion es invalida
        [Fact]
        public async Task Editar_Compra_CuandoEsInvalido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            //Establece el producto asociado a la compra
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 5,
                CostoProducto = 100
            };

            //Genera la nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = 1,
                UnidadesCompra = 5
            };

            controller.ModelState.AddModelError("UnidadesCompra", "El campo UnidadesCompra es requerido.");

            var result = await controller.Create(producto, compra);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Se comprueba que el dato ha sido actualizado
            var compraActualizada = await context.Compras.FirstOrDefaultAsync(c => c.ID == compra.ID);
            Assert.NotNull(compraActualizada);
            Assert.Equal(5, compraActualizada.UnidadesCompra);
        }

        // Obtener detalles de la compra cuando el ID es válido
        [Fact]
        public async Task Details_Compra_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            // Crea producto y proveedor asociados a la compra
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 5,
                CostoProducto = 100
            };
            context.Productos.Add(producto);

            var proveedor = new Proveedor
            {
                NombreProveedor = "MasPinturas"
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            // Genera la nueva compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = proveedor.ID,
                IdProducto = producto.ID,
                UnidadesCompra = 5
            };
            context.Compras.Add(compra);
            await context.SaveChangesAsync();

            // Verifica que el ID exista
            var result = await controller.Details(compra.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Compra>(viewResult.Model);
            Assert.Equal(compra.ID, returnValue.ID);
            Assert.Equal("Pintura gris", returnValue.Producto.NombreProducto);
        }


        // No obtiene los detalles del registro cuando el ID es inválido
        [Fact]
        public async Task Details_Compra_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        // Elimina el registro cuando el ID es válido
        [Fact]
        public async Task Eliminar_Compra_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            // Crea producto y proveedor asociados a la compra
            var producto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Pintura gris",
                DescripcionProducto = "Pintura para exteriores gris",
                UnidadesProducto = 5,
                CostoProducto = 100
            };
            context.Productos.Add(producto);

            var proveedor = new Proveedor
            {
                NombreProveedor = "MasPinturas"
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            // Crea compra
            var compra = new Compra
            {
                FechaCompra = DateTime.Now,
                IdProveedor = proveedor.ID,
                IdProducto = producto.ID,
                UnidadesCompra = 5
            };
            context.Compras.Add(compra);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(compra.ID);

            // Verificar que la compra ha sido eliminada
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var compraEnDb = context.Compras.FirstOrDefault(c => c.ID == compra.ID);
            Assert.Null(compraEnDb);
        }

        // No elimina el registro cuando el ID es inválido
        [Fact]
        public async Task Eliminar_Compra_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ComprasController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }


    }
}
