using Microsoft.EntityFrameworkCore;
using ProyectoDES_Empresa.Models;
using ProyectoDES_Empresa.Models.Seeds;

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

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Venta> Ventas { get; set; }


        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaSeed());
            modelBuilder.ApplyConfiguration(new CompraSeed());
            modelBuilder.ApplyConfiguration(new EmpleadoSeed());
            modelBuilder.ApplyConfiguration(new ProductoSeed());
            modelBuilder.ApplyConfiguration(new UsuarioSeed());
            modelBuilder.ApplyConfiguration(new ProveedorSeed());
            modelBuilder.ApplyConfiguration(new VentaSeed());

        }
    }
}
