using Microsoft.EntityFrameworkCore.Migrations;

namespace zv01.Data.Migrations
{
    public partial class borradosuave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaBorrado",
                table: "Reserva",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EstaBorrado",
                table: "Evento",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaBorrado",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "EstaBorrado",
                table: "Evento");
        }
    }
}
