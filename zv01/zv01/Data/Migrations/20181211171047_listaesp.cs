using Microsoft.EntityFrameworkCore.Migrations;

namespace zv01.Data.Migrations
{
    public partial class listaesp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListaEspera",
                table: "Evento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListaEspera",
                table: "Evento");
        }
    }
}
