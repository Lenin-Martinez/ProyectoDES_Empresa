using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoDES_Empresa.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCategoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionCategoria = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComisionVentaEmpleado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProveedor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescripcionProducto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnidadesProducto = table.Column<int>(type: "int", nullable: false),
                    CostoProducto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: true),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    UnidadesCompra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Compras_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: true),
                    UnidadesVenta = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitarioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioTotalVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ventas_Empleados_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ventas_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "ID", "DescripcionCategoria", "NombreCategoria" },
                values: new object[,]
                {
                    { 1, "Muebles artesanales de carpinteria", "Muebles de madera" },
                    { 2, "Camas de proveedores registrados", "Camas" },
                    { 3, "Aparatos electricos para el hogar", "Electrodomesticos" },
                    { 4, "Muebles y accesorios para niños y bebes", "Infantil" }
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "ID", "ApellidoEmpleado", "ComisionVentaEmpleado", "NombreEmpleado" },
                values: new object[,]
                {
                    { 1, "Perez", 1.50m, "Juan" },
                    { 2, "Torres", 1.0m, "Pamela" },
                    { 3, "Gomez", 2.0m, "Patricia" },
                    { 4, "Reyes", 1.5m, "Jonathan" },
                    { 5, "Torres", 3.0m, "Mardoqueo" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ID", "NombreProveedor" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Distribuidora El Sueño" },
                    { 3, "Almacenes La Moderna" },
                    { 4, "Carpinteria Don Mario" },
                    { 5, "Mabe" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "NombreRol" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ID", "CostoProducto", "DescripcionProducto", "IdCategoria", "NombreProducto", "UnidadesProducto" },
                values: new object[,]
                {
                    { 1, 124.99m, "1.00 m Cafe", 1, "Closet", 25 },
                    { 2, 154.99m, "1.40 m", 2, "Memory Foam", 50 },
                    { 3, 14.99m, "Mini Metalico", 3, "Ventilador", 100 },
                    { 4, 44.99m, "Rosado", 4, "Coche para bebe", 20 },
                    { 5, 104.99m, "Blanca", 4, "Cuna", 10 },
                    { 6, 164.99m, "1.40 m Natural", 1, "Chinero", 10 },
                    { 7, 174.99m, "32 Pulgadas Smart TV", 3, "Televisor", 30 },
                    { 8, 204.99m, "42 Pulgadas Smart TV", 3, "Televisor", 25 },
                    { 9, 134.99m, "1.20 m", 2, "Memory Foam", 35 },
                    { 10, 14.99m, "Mini Plastico", 3, "Ventilador", 100 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "ClaveUsuario", "CorreoUsuario", "IdRol" },
                values: new object[] { 1, "$2a$11$OFaXbiJ2RhLlxZECrVT0hOSWxK.btsic9Lhpop5hoOcRysJhMY4Jm", "admin@correo.com", 1 });

            migrationBuilder.InsertData(
                table: "Compras",
                columns: new[] { "ID", "FechaCompra", "IdProducto", "IdProveedor", "UnidadesCompra" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 25 },
                    { 2, new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 50 },
                    { 3, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 100 },
                    { 4, new DateTime(2024, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 20 },
                    { 5, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 10 },
                    { 6, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 10 }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "ID", "FechaVenta", "IdEmpleado", "IdProducto", "PrecioTotalVenta", "PrecioUnitarioVenta", "UnidadesVenta" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 150m, 150m, 1 },
                    { 2, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 200m, 200m, 1 },
                    { 3, new DateTime(2024, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 75m, 25m, 3 },
                    { 4, new DateTime(2024, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 60m, 60m, 1 },
                    { 5, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 125m, 125m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProducto",
                table: "Compras",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCategoria",
                table: "Productos",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRol",
                table: "Usuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdEmpleado",
                table: "Ventas",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdProducto",
                table: "Ventas",
                column: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
