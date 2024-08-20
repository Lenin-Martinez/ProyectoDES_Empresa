using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoDES_Empresa.Migrations
{
    /// <inheritdoc />
    public partial class Migracion_Inicial : Migration
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
                    NombreCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    NombreEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    NombreProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaveUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCategoria = table.Column<int>(type: "int", nullable: true),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescripcionProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { 1, "Pinturas de exteriores", "Exteriores" },
                    { 2, "pinturas de interiores", "Interiores" }
                });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "ID", "ApellidoEmpleado", "ComisionVentaEmpleado", "NombreEmpleado" },
                values: new object[,]
                {
                    { 1, "Perez", 1.50m, "Juan" },
                    { 2, "Torres", 1.0m, "Pamela" },
                    { 3, "Gomez", 0.8m, "Patricia" },
                    { 4, "Reyes", 1.2m, "Jonathan" },
                    { 5, "Torres", 0.10m, "Mardoqueo" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ID", "NombreProveedor" },
                values: new object[,]
                {
                    { 1, "Sherwin Williams" },
                    { 2, "Corona" },
                    { 3, "Comex" },
                    { 4, "Pinturas Americanas" },
                    { 5, "Pinturas genericas" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "ID", "ClaveUsuario", "CorreoUsuario" },
                values: new object[,]
                {
                    { 1, "1234", "lenin@hotmail.com" },
                    { 2, "1234", "omar@hotmail.com" },
                    { 3, "1234", "erika@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ID", "CostoProducto", "DescripcionProducto", "IdCategoria", "NombreProducto", "UnidadesProducto" },
                values: new object[,]
                {
                    { 1, 124.99m, "Pintura para exteriores azul", 1, "Pintura azul", 100 },
                    { 2, 124.99m, "Pintura para interiores Roja", 2, "Pintura Roja", 100 },
                    { 3, 124.99m, "Pintura para exteriores blanca", 1, "Pintura blanca", 100 },
                    { 4, 124.99m, "Pintura para interiores verde", 2, "Pintura verde", 100 },
                    { 5, 124.99m, "Pintura para exteriores marron", 1, "Pintura marron", 100 }
                });

            migrationBuilder.InsertData(
                table: "Compras",
                columns: new[] { "ID", "FechaCompra", "IdProducto", "IdProveedor", "UnidadesCompra" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 5 },
                    { 2, new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 10 },
                    { 3, new DateTime(2023, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 7 },
                    { 4, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 2 },
                    { 5, new DateTime(2023, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "ID", "FechaVenta", "IdEmpleado", "IdProducto", "PrecioTotalVenta", "PrecioUnitarioVenta", "UnidadesVenta" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 3874.69m, 124.99m, 31 },
                    { 2, new DateTime(2023, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1374m, 124.99m, 11 },
                    { 3, new DateTime(2023, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 609.95m, 124.99m, 5 },
                    { 4, new DateTime(2023, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 124.99m, 124.99m, 1 },
                    { 5, new DateTime(2023, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 249.98m, 124.99m, 2 }
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
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
