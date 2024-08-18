using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.Models;

namespace ProyectoDES_Empresa.Models
{
    public class EmpresaDBContext : DbContext
    {
        public EmpresaDBContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
    }
}
