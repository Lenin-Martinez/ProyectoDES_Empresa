using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class RolSeed : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasData(
                new Rol { ID = 1, NombreRol = "Admin" },
                new Rol { ID = 2, NombreRol = "Usuario" }
                );
        }
    }
}
