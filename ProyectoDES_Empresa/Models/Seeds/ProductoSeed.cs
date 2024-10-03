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
                  NombreProducto = "Closet",
                  DescripcionProducto = "1.00 m Cafe",
                  UnidadesProducto = 25,
                  CostoProducto = 124.99m
              },
               new Producto
               {
                   ID = 2,
                   IdCategoria = 2,
                   NombreProducto = "Memory Foam",
                   DescripcionProducto = "1.40 m",
                   UnidadesProducto = 50,
                   CostoProducto = 154.99m
               },
                new Producto
                {
                    ID = 3,
                    IdCategoria = 3,
                    NombreProducto = "Ventilador",
                    DescripcionProducto = "Mini Metalico",
                    UnidadesProducto = 100,
                    CostoProducto = 14.99m
                },
                 new Producto
                 {
                     ID = 4,
                     IdCategoria = 4,
                     NombreProducto = "Coche para bebe",
                     DescripcionProducto = "Rosado",
                     UnidadesProducto = 20,
                     CostoProducto = 44.99m
                 },
                  new Producto
                  {
                      ID = 5,
                      IdCategoria = 4,
                      NombreProducto = "Cuna",
                      DescripcionProducto = "Blanca",
                      UnidadesProducto = 10,
                      CostoProducto = 104.99m
                  },
                  new Producto
                  {
                      ID = 6,
                      IdCategoria = 1,
                      NombreProducto = "Chinero",
                      DescripcionProducto = "1.40 m Natural",
                      UnidadesProducto = 10,
                      CostoProducto = 164.99m
                  },
                  new Producto
                  {
                      ID = 7,
                      IdCategoria = 3,
                      NombreProducto = "Televisor",
                      DescripcionProducto = "32 Pulgadas Smart TV",
                      UnidadesProducto = 30,
                      CostoProducto = 174.99m
                  },
                  new Producto
                  {
                      ID = 8,
                      IdCategoria = 3,
                      NombreProducto = "Televisor",
                      DescripcionProducto = "42 Pulgadas Smart TV",
                      UnidadesProducto = 25,
                      CostoProducto = 204.99m
                  },
                   new Producto
                   {
                       ID = 9,
                       IdCategoria = 2,
                       NombreProducto = "Memory Foam",
                       DescripcionProducto = "1.20 m",
                       UnidadesProducto = 35,
                       CostoProducto = 134.99m
                   },
                    new Producto
                    {
                        ID = 10,
                        IdCategoria = 3,
                        NombreProducto = "Ventilador",
                        DescripcionProducto = "Mini Plastico",
                        UnidadesProducto = 100,
                        CostoProducto = 14.99m
                    }
                );
        }
    }
}
