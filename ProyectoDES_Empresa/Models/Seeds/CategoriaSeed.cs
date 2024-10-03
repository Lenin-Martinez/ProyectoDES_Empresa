using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class CategoriaSeed : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasData(
               new Categoria { ID = 1, NombreCategoria = "Muebles de madera", DescripcionCategoria = "Muebles artesanales de carpinteria"},
               new Categoria { ID = 2, NombreCategoria = "Camas", DescripcionCategoria = "Camas de proveedores registrados" },
               new Categoria { ID = 3, NombreCategoria = "Electrodomesticos", DescripcionCategoria = "Aparatos electricos para el hogar" },
               new Categoria { ID = 4, NombreCategoria = "Infantil", DescripcionCategoria = "Muebles y accesorios para niños y bebes" }
               );
        }
    }
}
