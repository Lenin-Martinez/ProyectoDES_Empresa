using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class VentaSeed
   : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.HasData(
                new Venta { 
                    ID = 1,
                    FechaVenta = DateTime.Parse("2023-11-25"),
                    IdProducto = 1,
                    UnidadesVenta = 31,
                    PrecioUnitarioVenta = 124.99m,
                    PrecioTotalVenta = 3874.69m,
                    IdEmpleado =  1

                },
                new Venta
                {
                    ID = 2,
                    FechaVenta = DateTime.Parse("2023-11-26"),
                    IdProducto = 2,
                    UnidadesVenta = 11,
                    PrecioUnitarioVenta = 124.99m,
                    PrecioTotalVenta = 1374m,
                    IdEmpleado = 2

                },
                new Venta
                {
                    ID = 3,
                    FechaVenta = DateTime.Parse("2023-11-26"),
                    IdProducto = 3,
                    UnidadesVenta = 5,
                    PrecioUnitarioVenta = 124.99m,
                    PrecioTotalVenta = 609.95m,
                    IdEmpleado = 3

                },
                new Venta
                {
                    ID = 4,
                    FechaVenta = DateTime.Parse("2023-11-27"),
                    IdProducto = 4,
                    UnidadesVenta = 1,
                    PrecioUnitarioVenta = 124.99m,
                    PrecioTotalVenta = 124.99m,
                    IdEmpleado = 4

                },
                new Venta
                {
                    ID = 5,
                    FechaVenta = DateTime.Parse("2023-11-28"),
                    IdProducto = 5,
                    UnidadesVenta = 2,
                    PrecioUnitarioVenta = 124.99m,
                    PrecioTotalVenta = 249.98m,
                    IdEmpleado = 5

                }

                );
        }
    }
}
