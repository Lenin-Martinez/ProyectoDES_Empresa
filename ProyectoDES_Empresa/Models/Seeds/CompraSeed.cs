using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class CompraSeed : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasData(
                 new Compra
                 {
                     ID = 1,
                     FechaCompra = DateTime.Parse("2024-08-01"),
                     IdProveedor = 4,
                     IdProducto = 1,
                     UnidadesCompra = 25
                 },
                 new Compra
                 {
                     ID = 2,
                     FechaCompra = DateTime.Parse("2024-08-22"),
                     IdProveedor = 2,
                     IdProducto = 2,
                     UnidadesCompra = 50
                 },
                 new Compra
                 {
                     ID = 3,
                     FechaCompra = DateTime.Parse("2024-07-14"),
                     IdProveedor = 3,
                     IdProducto = 3,
                     UnidadesCompra = 100
                 },
                 new Compra
                 {
                     ID = 4,
                     FechaCompra = DateTime.Parse("2024-09-14"),
                     IdProveedor = 3,
                     IdProducto = 4,
                     UnidadesCompra = 20
                 },
                 new Compra
                 {
                     ID = 5,
                     FechaCompra = DateTime.Parse("2024-08-04"),
                     IdProveedor = 3,
                     IdProducto = 5,
                     UnidadesCompra = 10
                 },
                 new Compra
                 {
                     ID = 6,
                     FechaCompra = DateTime.Parse("2024-03-04"),
                     IdProveedor = 4,
                     IdProducto = 6,
                     UnidadesCompra = 10
                 }



                );
        }
    }
}
