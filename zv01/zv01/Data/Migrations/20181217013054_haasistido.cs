using Microsoft.EntityFrameworkCore.Migrations;

namespace zv01.Data.Migrations
{
    public partial class haasistido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaAsistido",
                table: "Reserva",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaAsistido",
                table: "Reserva");
        }
    }
}
