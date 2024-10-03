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
                 new Venta
                 {
                     ID = 1,
                     FechaVenta = DateTime.Parse("2024-10-01"),
                     IdProducto = 1,
                     UnidadesVenta = 1,
                     PrecioUnitarioVenta = 150,
                     PrecioTotalVenta = 150,
                     IdEmpleado = 1
                 },
                  new Venta
                  {
                      ID = 2,
                      FechaVenta = DateTime.Parse("2024-10-02"),
                      IdProducto = 2,
                      UnidadesVenta = 1,
                      PrecioUnitarioVenta = 200,
                      PrecioTotalVenta = 200,
                      IdEmpleado = 2
                  },
                  new Venta
                  {
                      ID = 3,
                      FechaVenta = DateTime.Parse("2024-10-03"),
                      IdProducto = 3,
                      UnidadesVenta = 3,
                      PrecioUnitarioVenta = 25,
                      PrecioTotalVenta = 75,
                      IdEmpleado = 3
                  },
                  new Venta
                  {
                      ID = 4,
                      FechaVenta = DateTime.Parse("2024-10-04"),
                      IdProducto = 4,
                      UnidadesVenta = 1,
                      PrecioUnitarioVenta = 60,
                      PrecioTotalVenta = 60,
                      IdEmpleado = 5
                  },
                  new Venta
                  {
                      ID = 5,
                      FechaVenta = DateTime.Parse("2024-10-05"),
                      IdProducto = 5,
                      UnidadesVenta = 1,
                      PrecioUnitarioVenta = 125,
                      PrecioTotalVenta = 125,
                      IdEmpleado = 5
                  }
                 );
        }
    }
}
