using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class ProductoSeed : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasData(
              new Producto { ID = 1,
                  IdCategoria = 1,
                  NombreProducto = "Pintura azul",
                  DescripcionProducto = "Pintura para exteriores azul",
                  UnidadesProducto = 100,
                  CostoProducto = 124.99m
              },
               new Producto
               {
                   ID = 2,
                   IdCategoria = 2,
                   NombreProducto = "Pintura Roja",
                   DescripcionProducto = "Pintura para interiores Roja",
                   UnidadesProducto = 100,
                   CostoProducto = 124.99m
               },
                new Producto
                {
                    ID = 3,
                    IdCategoria = 1,
                    NombreProducto = "Pintura blanca",
                    DescripcionProducto = "Pintura para exteriores blanca",
                    UnidadesProducto = 100,
                    CostoProducto = 124.99m
                },
                 new Producto
                 {
                     ID = 4,
                     IdCategoria = 2,
                     NombreProducto = "Pintura verde",
                     DescripcionProducto = "Pintura para interiores verde",
                     UnidadesProducto = 100,
                     CostoProducto = 124.99m
                 },
                  new Producto
                  {
                      ID = 5,
                      IdCategoria = 1,
                      NombreProducto = "Pintura marron",
                      DescripcionProducto = "Pintura para exteriores marron",
                      UnidadesProducto = 100,
                      CostoProducto = 124.99m
                  }

                );
        }
    }
}
