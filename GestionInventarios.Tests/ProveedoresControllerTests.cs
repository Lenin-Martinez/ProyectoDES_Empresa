using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarios.Tests
{
    public class ProveedoresControllerTests
    {
        // Ingresa el proveedor cuando la información es válida
        [Fact]
        public async Task Crear_Proveedor_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            var proveedor = new Proveedor
            {
                NombreProveedor = "ProPinturas",
            };

            var result = await controller.Create(proveedor);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var proveedorCreado = await context.Proveedores.FirstOrDefaultAsync(c => c.NombreProveedor == "ProPinturas");
            Assert.NotNull(proveedorCreado);
            Assert.Equal("ProPinturas", proveedorCreado.NombreProveedor);
        }

        // No ingresa el proveedor cuando el modelo no es válido
        [Fact]
        public async Task Crear_Proveedor_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            var proveedor = new Proveedor
            {
                NombreProveedor = "",
            };

            // Error de validación en el Nombre de Proveedor
            controller.ModelState.AddModelError("NombreProveedor", "El nombre es requerido.");

            var result = await controller.Create(proveedor);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Proveedor>(viewResult.Model);
            Assert.Equal(proveedor.NombreProveedor, returnValue.NombreProveedor);
        }

        //Edita el proveedor cuando es valida la informacion
        [Fact]
        public async Task Editar_Post_Proveedor_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            //Crea un nuevo proveedor
            var proveedor = new Proveedor
            {
                NombreProveedor = "ProPinturas",
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            //Actualizacion de parametros
            proveedor.NombreProveedor = "Super Pinturas";

            var result = await controller.Edit(proveedor.ID, proveedor);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Verifica que la informacion ha sido actualizada
            var proveedorActualizado = context.Proveedores.FirstOrDefault(p => p.ID == proveedor.ID);
            Assert.NotNull(proveedorActualizado);
            Assert.Equal("Super Pinturas", proveedorActualizado.NombreProveedor);
        }

        //No Actualiza el proveedor cuando la informacion no es valida
        [Fact]
        public async Task Editar_Post_Proveedor_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            //Crea un nuevo proveedor
            var proveedor = new Proveedor
            {
                NombreProveedor = "ProPinturas",
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            // Error de validación
            controller.ModelState.AddModelError("NombreProveedor", "El nombre es requerido.");

            var result = await controller.Edit(proveedor.ID, proveedor);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Proveedor>(viewResult.Model);
            Assert.Equal("ProPinturas", returnValue.NombreProveedor);
        }

        //Obtener detalles del proveedor cuando el Id es Valido
        [Fact]
        public async Task Details_Proveedor_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            //Crea proveedor
            var proveedor = new Proveedor
            {
                NombreProveedor = "ProPinturas",
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            //Verifica que el ID exista
            var result = await controller.Details(proveedor.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Proveedor>(viewResult.Model);
            Assert.Equal("ProPinturas", returnValue.NombreProveedor);
        }

        //No obtiene los detalles del registro cuando el id es invalido
        [Fact]
        public async Task Details_Proveedor_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        //Elimina el registro cuando el Id es valido
        [Fact]
        public async Task Eliminar_Proveedor_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            //Crea nueva categoria
            var proveedor = new Proveedor
            {
                NombreProveedor = "ProPinturas",
            };
            context.Proveedores.Add(proveedor);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(proveedor.ID);

            // Verificar que el proveedor ha sido eliminado
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var proveedorEnDb = context.Proveedores.FirstOrDefault(p => p.ID == proveedor.ID);
            Assert.Null(proveedorEnDb);
        }

        //No elimina el registro cuando el Id es invalido
        [Fact]
        public async Task Eliminar_Proveedor_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new ProveedoresController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }



    }
}
