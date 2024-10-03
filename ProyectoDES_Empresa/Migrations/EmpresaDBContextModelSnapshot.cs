﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoDES_Empresa.Models;

#nullable disable

namespace ProyectoDES_Empresa.Migrations
{
    [DbContext(typeof(EmpresaDBContext))]
    partial class EmpresaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Categoria", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("DescripcionCategoria")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Categorias");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DescripcionCategoria = "Muebles artesanales de carpinteria",
                            NombreCategoria = "Muebles de madera"
                        },
                        new
                        {
                            ID = 2,
                            DescripcionCategoria = "Camas de proveedores registrados",
                            NombreCategoria = "Camas"
                        },
                        new
                        {
                            ID = 3,
                            DescripcionCategoria = "Aparatos electricos para el hogar",
                            NombreCategoria = "Electrodomesticos"
                        },
                        new
                        {
                            ID = 4,
                            DescripcionCategoria = "Muebles y accesorios para niños y bebes",
                            NombreCategoria = "Infantil"
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Compra", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int?>("IdProveedor")
                        .HasColumnType("int");

                    b.Property<int>("UnidadesCompra")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdProveedor");

                    b.ToTable("Compras");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FechaCompra = new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 1,
                            IdProveedor = 4,
                            UnidadesCompra = 25
                        },
                        new
                        {
                            ID = 2,
                            FechaCompra = new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 2,
                            IdProveedor = 2,
                            UnidadesCompra = 50
                        },
                        new
                        {
                            ID = 3,
                            FechaCompra = new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 3,
                            IdProveedor = 3,
                            UnidadesCompra = 100
                        },
                        new
                        {
                            ID = 4,
                            FechaCompra = new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 4,
                            IdProveedor = 3,
                            UnidadesCompra = 20
                        },
                        new
                        {
                            ID = 5,
                            FechaCompra = new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 5,
                            IdProveedor = 3,
                            UnidadesCompra = 10
                        },
                        new
                        {
                            ID = 6,
                            FechaCompra = new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdProducto = 6,
                            IdProveedor = 4,
                            UnidadesCompra = 10
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Empleado", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ApellidoEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("ComisionVentaEmpleado")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Empleados");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ApellidoEmpleado = "Perez",
                            ComisionVentaEmpleado = 1.50m,
                            NombreEmpleado = "Juan"
                        },
                        new
                        {
                            ID = 2,
                            ApellidoEmpleado = "Torres",
                            ComisionVentaEmpleado = 1.0m,
                            NombreEmpleado = "Pamela"
                        },
                        new
                        {
                            ID = 3,
                            ApellidoEmpleado = "Gomez",
                            ComisionVentaEmpleado = 2.0m,
                            NombreEmpleado = "Patricia"
                        },
                        new
                        {
                            ID = 4,
                            ApellidoEmpleado = "Reyes",
                            ComisionVentaEmpleado = 1.5m,
                            NombreEmpleado = "Jonathan"
                        },
                        new
                        {
                            ID = 5,
                            ApellidoEmpleado = "Torres",
                            ComisionVentaEmpleado = 3.0m,
                            NombreEmpleado = "Mardoqueo"
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Producto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CostoProducto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DescripcionProducto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<string>("NombreProducto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UnidadesProducto")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CostoProducto = 124.99m,
                            DescripcionProducto = "1.00 m Cafe",
                            IdCategoria = 1,
                            NombreProducto = "Closet",
                            UnidadesProducto = 25
                        },
                        new
                        {
                            ID = 2,
                            CostoProducto = 154.99m,
                            DescripcionProducto = "1.40 m",
                            IdCategoria = 2,
                            NombreProducto = "Memory Foam",
                            UnidadesProducto = 50
                        },
                        new
                        {
                            ID = 3,
                            CostoProducto = 14.99m,
                            DescripcionProducto = "Mini Metalico",
                            IdCategoria = 3,
                            NombreProducto = "Ventilador",
                            UnidadesProducto = 100
                        },
                        new
                        {
                            ID = 4,
                            CostoProducto = 44.99m,
                            DescripcionProducto = "Rosado",
                            IdCategoria = 4,
                            NombreProducto = "Coche para bebe",
                            UnidadesProducto = 20
                        },
                        new
                        {
                            ID = 5,
                            CostoProducto = 104.99m,
                            DescripcionProducto = "Blanca",
                            IdCategoria = 4,
                            NombreProducto = "Cuna",
                            UnidadesProducto = 10
                        },
                        new
                        {
                            ID = 6,
                            CostoProducto = 164.99m,
                            DescripcionProducto = "1.40 m Natural",
                            IdCategoria = 1,
                            NombreProducto = "Chinero",
                            UnidadesProducto = 10
                        },
                        new
                        {
                            ID = 7,
                            CostoProducto = 174.99m,
                            DescripcionProducto = "32 Pulgadas Smart TV",
                            IdCategoria = 3,
                            NombreProducto = "Televisor",
                            UnidadesProducto = 30
                        },
                        new
                        {
                            ID = 8,
                            CostoProducto = 204.99m,
                            DescripcionProducto = "42 Pulgadas Smart TV",
                            IdCategoria = 3,
                            NombreProducto = "Televisor",
                            UnidadesProducto = 25
                        },
                        new
                        {
                            ID = 9,
                            CostoProducto = 134.99m,
                            DescripcionProducto = "1.20 m",
                            IdCategoria = 2,
                            NombreProducto = "Memory Foam",
                            UnidadesProducto = 35
                        },
                        new
                        {
                            ID = 10,
                            CostoProducto = 14.99m,
                            DescripcionProducto = "Mini Plastico",
                            IdCategoria = 3,
                            NombreProducto = "Ventilador",
                            UnidadesProducto = 100
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Proveedor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("NombreProveedor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Proveedores");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            NombreProveedor = "Samsung"
                        },
                        new
                        {
                            ID = 2,
                            NombreProveedor = "Distribuidora El Sueño"
                        },
                        new
                        {
                            ID = 3,
                            NombreProveedor = "Almacenes La Moderna"
                        },
                        new
                        {
                            ID = 4,
                            NombreProveedor = "Carpinteria Don Mario"
                        },
                        new
                        {
                            ID = 5,
                            NombreProveedor = "Mabe"
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Rol", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            NombreRol = "Admin"
                        },
                        new
                        {
                            ID = 2,
                            NombreRol = "Usuario"
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Usuario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ClaveUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdRol")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdRol");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            ClaveUsuario = "$2a$11$OFaXbiJ2RhLlxZECrVT0hOSWxK.btsic9Lhpop5hoOcRysJhMY4Jm",
                            CorreoUsuario = "admin@correo.com",
                            IdRol = 1
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Venta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int?>("IdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioTotalVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecioUnitarioVenta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnidadesVenta")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdProducto");

                    b.ToTable("Ventas");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FechaVenta = new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdEmpleado = 1,
                            IdProducto = 1,
                            PrecioTotalVenta = 150m,
                            PrecioUnitarioVenta = 150m,
                            UnidadesVenta = 1
                        },
                        new
                        {
                            ID = 2,
                            FechaVenta = new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdEmpleado = 2,
                            IdProducto = 2,
                            PrecioTotalVenta = 200m,
                            PrecioUnitarioVenta = 200m,
                            UnidadesVenta = 1
                        },
                        new
                        {
                            ID = 3,
                            FechaVenta = new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdEmpleado = 3,
                            IdProducto = 3,
                            PrecioTotalVenta = 75m,
                            PrecioUnitarioVenta = 25m,
                            UnidadesVenta = 3
                        },
                        new
                        {
                            ID = 4,
                            FechaVenta = new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdEmpleado = 5,
                            IdProducto = 4,
                            PrecioTotalVenta = 60m,
                            PrecioUnitarioVenta = 60m,
                            UnidadesVenta = 1
                        },
                        new
                        {
                            ID = 5,
                            FechaVenta = new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IdEmpleado = 5,
                            IdProducto = 5,
                            PrecioTotalVenta = 125m,
                            PrecioUnitarioVenta = 125m,
                            UnidadesVenta = 1
                        });
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Compra", b =>
                {
                    b.HasOne("ProyectoDES_Empresa.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto");

                    b.HasOne("ProyectoDES_Empresa.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("IdProveedor");

                    b.Navigation("Producto");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Producto", b =>
                {
                    b.HasOne("ProyectoDES_Empresa.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdCategoria");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Usuario", b =>
                {
                    b.HasOne("ProyectoDES_Empresa.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("ProyectoDES_Empresa.Models.Venta", b =>
                {
                    b.HasOne("ProyectoDES_Empresa.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado");

                    b.HasOne("ProyectoDES_Empresa.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto");

                    b.Navigation("Empleado");

                    b.Navigation("Producto");
                });
#pragma warning restore 612, 618
        }
    }
}
