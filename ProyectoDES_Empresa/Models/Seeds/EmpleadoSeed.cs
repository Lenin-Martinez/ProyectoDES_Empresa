using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class EmpleadoSeed : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.HasData(
                new Empleado {
                    ID = 1,
                    NombreEmpleado = "Juan",
                    ApellidoEmpleado = "Perez",
                    ComisionVentaEmpleado = 1.50m

                },
                 new Empleado
                 {
                     ID = 2,
                     NombreEmpleado = "Pamela",
                     ApellidoEmpleado = "Torres",
                     ComisionVentaEmpleado = 1.0m

                 },
                  new Empleado
                  {
                      ID = 3,
                      NombreEmpleado = "Patricia",
                      ApellidoEmpleado = "Gomez",
                      ComisionVentaEmpleado = 2.0m

                  },
                   new Empleado
                   {
                       ID = 4,
                       NombreEmpleado = "Jonathan",
                       ApellidoEmpleado = "Reyes",
                       ComisionVentaEmpleado = 1.5m

                   },
                    new Empleado
                    {
                        ID = 5,
                        NombreEmpleado = "Mardoqueo",
                        ApellidoEmpleado = "Torres",
                        ComisionVentaEmpleado = 3.0m

                    }
                );
        }
    }
}
