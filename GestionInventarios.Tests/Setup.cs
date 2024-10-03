using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.Models;

namespace GestionInventarios.Tests
{
    public static class Setup
    {
        public static EmpresaDBContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EmpresaDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new EmpresaDBContext(options);
            context.Database.EnsureCreated();
            return context;
        }

    }
}
