using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarios.Tests
{
    public class EmpleadosControllerTests
    {
        //Ingresa el empleado cuando la informacion es valida
        [Fact]
        public async Task Crear_Empleado_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            var empleado = new Empleado
            {
                NombreEmpleado = "Adonay",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };

            var result = await controller.Create(empleado);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var empleadoCreada = await context.Empleados.FirstOrDefaultAsync(c => c.NombreEmpleado == "Adonay");
            Assert.NotNull(empleadoCreada);
            Assert.Equal("Adonay", empleadoCreada.NombreEmpleado);
            Assert.Equal("Lopez", empleadoCreada.ApellidoEmpleado);
        }

        //No ingresa el empleado cuando el modelo no es valido
        [Fact]
        public async Task Crear_Empleado_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            var empleado = new Empleado
            {
                NombreEmpleado = "",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };

            // Error de validación en el Nombre de Empleado
            controller.ModelState.AddModelError("NombreEmpleado", "El nombre es requerido.");

            var result = await controller.Create(empleado);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Empleado>(viewResult.Model);
            Assert.Equal(empleado.NombreEmpleado, returnValue.NombreEmpleado);
        }

        //Edita el empleado cuando la informacion es valida
        [Fact]
        public async Task Editar_Post_Empleado_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            //Crea un nuevo empleado
            var empleado = new Empleado
            {
                NombreEmpleado = "Adonay",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            //Actualizacion de parametros
            empleado.NombreEmpleado = "Carlos";
            empleado.ApellidoEmpleado = "Torres";

            var result = await controller.Edit(empleado.ID, empleado);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Verifica que la informacion ha sido actualizada
            var èmpleadoActualizado = context.Empleados.FirstOrDefault(p => p.ID == empleado.ID);
            Assert.NotNull(èmpleadoActualizado);
            Assert.Equal("Carlos", èmpleadoActualizado.NombreEmpleado);
            Assert.Equal("Torres", èmpleadoActualizado.ApellidoEmpleado);
        }

        //No Actualiza el empleado cuando la informacion no es valida
        [Fact]
        public async Task Editar_Post_Empleado_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            //Crea una nuevo empleado
            var empleado = new Empleado
            {
                NombreEmpleado = "Adonay",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            // Error de validación
            controller.ModelState.AddModelError("NombreEmpleado", "El nombre es requerido.");

            var result = await controller.Edit(empleado.ID, empleado);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Empleado>(viewResult.Model);
            Assert.Equal("Adonay", returnValue.NombreEmpleado);
        }

        //Obtener detalles del empleado cuando el Id es Valido
        [Fact]
        public async Task Details_Empleado_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            //Crea empleado
            var empleado = new Empleado
            {
                NombreEmpleado = "Adonay",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            //Verifica que el ID exista
            var result = await controller.Details(empleado.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Empleado>(viewResult.Model);
            Assert.Equal("Adonay", returnValue.NombreEmpleado);
            Assert.Equal("Lopez", returnValue.ApellidoEmpleado);
        }

        //No obtiene los detalles del registro cuando el id es invalido
        [Fact]
        public async Task Details_Empleado_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        //Elimina el registro cuando el Id es valido
        [Fact]
        public async Task Eliminar_Empleado_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            //Crea empleado
            var empleado = new Empleado
            {
                NombreEmpleado = "Adonay",
                ApellidoEmpleado = "Lopez",
                ComisionVentaEmpleado = 5
            };
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(empleado.ID);

            // Verificar que el empleado ha sido eliminado
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var empleadoEnDb = context.Empleados.FirstOrDefault(p => p.ID == empleado.ID);
            Assert.Null(empleadoEnDb);
        }

        //No elimina el registro cuando el Id es invalido
        [Fact]
        public async Task Eliminar_Empleado_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new EmpleadosController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
