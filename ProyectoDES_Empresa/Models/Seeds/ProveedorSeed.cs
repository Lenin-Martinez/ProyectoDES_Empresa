using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class ProveedorSeed : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.HasData(
                new Proveedor { ID = 1, NombreProveedor ="Sherwin Williams" },
                new Proveedor { ID = 2, NombreProveedor = "Corona" },
                new Proveedor { ID = 3, NombreProveedor = "Comex" },
                new Proveedor { ID = 4, NombreProveedor = "Pinturas Americanas"},
                 new Proveedor { ID = 5, NombreProveedor = "Pinturas genericas" }
                );
        }
    }
}
