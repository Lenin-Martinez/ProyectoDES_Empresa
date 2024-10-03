using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class UsuarioSeed
  : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(
                new Usuario { ID = 1, CorreoUsuario = "admin@correo.com", ClaveUsuario = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd"), IdRol = 1 }
                );
        }
    }
}
