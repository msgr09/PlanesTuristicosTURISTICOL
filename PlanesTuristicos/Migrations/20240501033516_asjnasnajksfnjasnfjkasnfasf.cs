using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanesTuristicos.Migrations
{
    /// <inheritdoc />
    public partial class asjnasnajksfnjasnfjkasnfasf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id_Proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_turista = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Rut = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: true),
                    cedula = table.Column<int>(type: "int", nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Clave = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedores__Id", x => x.Id_Proveedor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_turista = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Clave = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF971AB28277", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "PlanesT",
                columns: table => new
                {
                    Id_PlanTuristicos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_PlanTuristico = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Rut = table.Column<int>(type: "int", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", unicode: false, maxLength: 50, nullable: false),
                    Actividades = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: true),
                    Duracion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Informacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Imagen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlanesT__Id", x => x.Id_PlanTuristicos);
                    table.ForeignKey(
                        name: "FK_PlanesT_Proveedor_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "Id_Proveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlanTuristico = table.Column<int>(type: "int", nullable: false),
                    NombrePlanTuristico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reserva_PlanesT",
                        column: x => x.IdPlanTuristico,
                        principalTable: "PlanesT",
                        principalColumn: "Id_PlanTuristicos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanesT_IdProveedor",
                table: "PlanesT",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_IdPlanTuristico",
                table: "Reserva",
                column: "IdPlanTuristico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "PlanesT");

            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
