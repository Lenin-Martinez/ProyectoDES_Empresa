﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDES_Empresa.Models.Seeds
{
    public class ProveedorSeed : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.HasData(
                new Proveedor { ID = 1, NombreProveedor = "Samsung" },
                new Proveedor { ID = 2, NombreProveedor = "Distribuidora El Sueño" },
                new Proveedor { ID = 3, NombreProveedor = "Almacenes La Moderna" },
                new Proveedor { ID = 4, NombreProveedor = "Carpinteria Don Mario" },
                new Proveedor { ID = 5, NombreProveedor = "Mabe" }
                );
        }
    }
}
