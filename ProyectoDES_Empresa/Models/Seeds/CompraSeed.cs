using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class CompraSeed : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasData(
                new Compra { ID = 1, 
                    FechaCompra = DateTime.Parse("2023-11-17"),
                    IdProveedor = 1,
                    IdProducto = 1,
                    UnidadesCompra = 5

                },
                 new Compra
                 {
                     ID = 2,
                     FechaCompra = DateTime.Parse("2023-11-18"),
                     IdProveedor = 2,
                     IdProducto = 2,
                     UnidadesCompra = 10

                 },
                 new Compra
                 {
                     ID = 3,
                     FechaCompra = DateTime.Parse("2023-11-19"),
                     IdProveedor = 3,
                     IdProducto = 3,
                     UnidadesCompra = 7

                 },
                 new Compra
                 {
                     ID = 4,
                     FechaCompra = DateTime.Parse("2023-11-20"),
                     IdProveedor = 4,
                     IdProducto = 4,
                     UnidadesCompra = 2

                 },
                 new Compra
                 {
                     ID = 5,
                     FechaCompra = DateTime.Parse("2023-11-21"),
                     IdProveedor = 5,
                     IdProducto = 5,
                     UnidadesCompra = 1

                 }


                );
        }
    }
}
