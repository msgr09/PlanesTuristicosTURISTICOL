using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanesTuristicos.Migrations
{
    /// <inheritdoc />
    public partial class djfjsdbfjksdbfsdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actividades",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Reserva",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Informacion",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Precio",
                table: "Reserva",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actividades",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "Informacion",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Reserva");
        }
    }
}
