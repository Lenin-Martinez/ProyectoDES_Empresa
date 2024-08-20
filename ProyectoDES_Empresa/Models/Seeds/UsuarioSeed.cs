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
                new Usuario { ID = 1 , CorreoUsuario ="lenin@hotmail.com",ClaveUsuario = "1234"},
                new Usuario { ID = 2, CorreoUsuario = "omar@hotmail.com", ClaveUsuario = "1234" },
                new Usuario { ID = 3, CorreoUsuario = "erika@hotmail.com", ClaveUsuario = "1234" }
                );
        }
    }
}
