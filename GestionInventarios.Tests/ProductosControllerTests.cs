using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestionInventarios.Tests
{
    public class ProductosControllerTests
    {
        //Ingresar a base de datos un producto nuevo con informacion valida.
        [Fact]
        public async Task Post_Agregar_Producto_CuandoEsValido_CuandoEsNuevo()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var nuevoProducto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Gavetero",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 10,
                CostoProducto = 124.99m
            };

            var result = await controller.Create(nuevoProducto);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var productoEnDb = context.Productos.FirstOrDefault(p => p.NombreProducto == "Gavetero" && p.IdCategoria == 1);
            Assert.NotNull(productoEnDb);
            Assert.Equal("Gavetero", productoEnDb.NombreProducto);
            Assert.Equal(10, productoEnDb.UnidadesProducto);
            Assert.Equal(124.99m, productoEnDb.CostoProducto);
        }

        //No Ingresar a base de datos un producto nuevo con informacion no valida.
        [Fact]
        public async Task Post_Agregar_Producto_CuandoNoEsValido_CuandoEsNuevo()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var nuevoProducto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m
            };

            //Envia el producto de manera invalida
            controller.ModelState.AddModelError("NombreProducto", "Required");

            var result = await controller.Create(nuevoProducto);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Producto>(viewResult.Model);
            Assert.Equal("Closet", model.NombreProducto);
            Assert.False(controller.ModelState.IsValid);
        }


        //Ingresar a una base de datos un producto que ya posee registro
        [Fact]
        public async Task Post_Agregar_Producto_CuandoEsValido_CuandoYaExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var productoExistente = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Gavetero",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 50,
                CostoProducto = 124.99m
            };
            context.Productos.Add(productoExistente);
            await context.SaveChangesAsync();

            var nuevoProducto = new Producto
            {
                IdCategoria = 1,
                NombreProducto = "Gavetero",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m
            };

            var result = await controller.Create(nuevoProducto);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var productoEnDb = context.Productos.FirstOrDefault(p => p.NombreProducto == "Gavetero" && p.IdCategoria == 1);
            Assert.NotNull(productoEnDb);
            Assert.Equal("Gavetero", productoEnDb.NombreProducto);
            //Valida la suma de las unidades nuevas con las existentes.
            Assert.Equal(150, productoEnDb.UnidadesProducto);
            Assert.Equal(124.99m, productoEnDb.CostoProducto);
        }

        //Retorna la informacion correcta de un producto cuando el ID es valido
        [Fact]
        public async Task GetDetails_RetornaProducto_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var producto = new Producto
            {
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m,
                IdCategoria = 1
            };
            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            var result = await controller.Details(producto.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Producto>(viewResult.Model);
            Assert.Equal("Closet", returnValue.NombreProducto);
        }

        //Cuando el ID es no valido, responde con un NotFound
        [Fact]
        public async Task GetProductoDetails_RetornaNotFound_CuandoIdNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        //Edita cuando la informacion nueva es valida para la actualizacion
        [Fact]
        public async Task Edit_Post_Producto_CuandoActualizacionEsValida()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var producto = new Producto
            {
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m,
                IdCategoria = 1
            };
            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            //Actulizacion de parametros
            producto.NombreProducto = "Chinero";
            producto.DescripcionProducto = "1.50 Cafe";

            var result = await controller.Edit(producto.ID, producto);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var productoEnDb = context.Productos.FirstOrDefault(p => p.ID == producto.ID);
            Assert.NotNull(productoEnDb);
            Assert.Equal("Chinero", productoEnDb.NombreProducto);
            Assert.Equal("1.50 Cafe", productoEnDb.DescripcionProducto);
        }

        //No edita el registro cuando la informacion es invalida para la actualizacion
        [Fact]
        public async Task Edit_Post_Producto_CuandoActualizacionNoEsValida()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var producto = new Producto
            {
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m,
                IdCategoria = 1
            };
            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            //Se envia el modelo con un error de validacion
            controller.ModelState.AddModelError("NombreProducto", "Required");

            var result = await controller.Edit(producto.ID, producto);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Producto>(viewResult.Model);
            Assert.Equal("Closet", returnValue.NombreProducto);
        }

        //Elimina cuando el ID es valido y otras tablas no dependen de este registro
        [Fact]
        public async Task Delete_Producto_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);
            var producto = new Producto
            {
                NombreProducto = "Closet",
                DescripcionProducto = "1.00 Blanco",
                UnidadesProducto = 100,
                CostoProducto = 124.99m,
                IdCategoria = 1
            };
            context.Productos.Add(producto);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(producto.ID);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var productoEnDb = context.Productos.FirstOrDefault(p => p.ID == producto.ID);
            Assert.Null(productoEnDb);
        }

        //No elimina el registro porque el ID no es valido, retorna notFound
        [Fact]
        public async Task Delete_Producto_CuandoIdNoExiste()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProductosController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
