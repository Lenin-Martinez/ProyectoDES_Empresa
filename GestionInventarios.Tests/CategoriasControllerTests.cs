using ProyectoDES_Empresa.Controllers;
using ProyectoDES_Empresa.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GestionInventarios.Tests
{
    public class CategoriasControllerTests
    {
        //Ingresa la categoria cuando la informacion es valida
        [Fact]
        public async Task Crear_Categoria_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            var categoria = new Categoria
            {
                NombreCategoria = "Juego de Sala",
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };

            var result = await controller.Create(categoria);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var categoriaCreada = await context.Categorias.FirstOrDefaultAsync(c => c.NombreCategoria == "Juego de Sala");
            Assert.NotNull(categoriaCreada);
            Assert.Equal("Juego de Sala", categoriaCreada.NombreCategoria);
        }

        //No ingresa la categoria cuando el modelo no es valido
        [Fact]
        public async Task Crear_Categoria_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            var categoria = new Categoria
            {
                NombreCategoria = "", 
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };

            // Error de validación en el Nombre de Categoria
            controller.ModelState.AddModelError("NombreCategoria", "El nombre es requerido.");

            var result = await controller.Create(categoria);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Categoria>(viewResult.Model);
            Assert.Equal(categoria.DescripcionCategoria, returnValue.DescripcionCategoria);
        }

        //Edita la categoria cuando es valida la informacion
        [Fact]
        public async Task Editar_Post_Categoria_CuandoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            //Crea una nueva categoria
            var categoria = new Categoria
            {
                NombreCategoria = "Juego de Sala",
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            //Actualizacion de parametros
            categoria.NombreCategoria = "Producto Metalico";
            categoria.DescripcionCategoria = "Diversos productos metalicos: estantes, comedores, percheros.";

            var result = await controller.Edit(categoria.ID, categoria);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            //Verifica que la informacion ha sido actualizada
            var categoriaActualizada = context.Categorias.FirstOrDefault(p => p.ID == categoria.ID);
            Assert.NotNull(categoriaActualizada);
            Assert.Equal("Producto Metalico", categoriaActualizada.NombreCategoria);
            Assert.Equal("Diversos productos metalicos: estantes, comedores, percheros.", categoriaActualizada.DescripcionCategoria);
        }

        //No Actualiza la categoria cuando la informacion no es valida
        [Fact]
        public async Task Editar_Post_Categoria_CuandoNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            //Crea una nueva categoria
            var categoria = new Categoria
            {
                NombreCategoria = "Juego de Sala",
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            // Error de validación
            controller.ModelState.AddModelError("NombreCategoria", "El nombre es requerido.");

            var result = await controller.Edit(categoria.ID, categoria);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Categoria>(viewResult.Model);
            Assert.Equal("Juego de Sala", returnValue.NombreCategoria);
        }

        //Obtener detalles de la categoria cuando el Id es Valido
        [Fact]
        public async Task Details_Categoria_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            //Crea categoria
            var categoria = new Categoria
            {
                NombreCategoria = "Juego de Sala",
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            //Verifica que el ID exista
            var result = await controller.Details(categoria.ID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var returnValue = Assert.IsType<Categoria>(viewResult.Model);
            Assert.Equal("Juego de Sala", returnValue.NombreCategoria);
        }

        //No obtiene los detalles del registro cuando el id es invalido
        [Fact]
        public async Task Details_Categoria_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        //Elimina el registro cuando el Id es valido
        [Fact]
        public async Task Eliminar_Categoria_CuandoIdEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            //Crea nueva categoria
            var categoria = new Categoria
            {
                NombreCategoria = "Juego de Sala",
                DescripcionCategoria = "Juegos de sala de diversos modelos y tamaños"
            };
            context.Categorias.Add(categoria);
            await context.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(categoria.ID);

            // Verificar que la categoría ha sido eliminada
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(controller.Index), redirectResult.ActionName);

            var categoriaEnDb = context.Categorias.FirstOrDefault(p => p.ID == categoria.ID);
            Assert.Null(categoriaEnDb);
        }

        //No elimina el registro cuando el Id es invalido
        [Fact]
        public async Task Eliminar_Categoria_CuandoIdNoEsValido()
        {
            var context = Setup.GetInMemoryDatabaseContext();
            var controller = new CategoriasController(context);

            var result = await controller.DeleteConfirmed(999);

            Assert.IsType<NotFoundResult>(result);
        }


    }
}
