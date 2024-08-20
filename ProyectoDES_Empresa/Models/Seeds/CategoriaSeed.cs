using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class CategoriaSeed : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasData(
                new Categoria { ID = 1, NombreCategoria = "Exteriores", DescripcionCategoria = "Pinturas de exteriores"},
               new Categoria { ID = 2, NombreCategoria = "Interiores", DescripcionCategoria = "pinturas de interiores" }
              
                );
        }
    }
}
