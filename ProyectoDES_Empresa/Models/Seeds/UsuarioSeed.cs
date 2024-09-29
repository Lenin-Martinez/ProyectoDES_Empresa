using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class UsuarioSeed
  : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(
                new Usuario { ID = 1, CorreoUsuario = "admin@hotmail.com", ClaveUsuario = "1234", IdRol = 1 },
                new Usuario { ID = 2, CorreoUsuario = "lenin@hotmail.com", ClaveUsuario = "1234", IdRol = 2 }
                );
        }
    }
}
