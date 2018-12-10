using Microsoft.EntityFrameworkCore.Migrations;

namespace zv01.Data.Migrations
{
    public partial class pr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_EstadoReserva_EstadoReservaId",
                table: "Reserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoReserva",
                table: "EstadoReserva");

            migrationBuilder.RenameTable(
                name: "EstadoReserva",
                newName: "EstadoReservas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoReservas",
                table: "EstadoReservas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_EstadoReservas_EstadoReservaId",
                table: "Reserva",
                column: "EstadoReservaId",
                principalTable: "EstadoReservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_EstadoReservas_EstadoReservaId",
                table: "Reserva");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoReservas",
                table: "EstadoReservas");

            migrationBuilder.RenameTable(
                name: "EstadoReservas",
                newName: "EstadoReserva");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoReserva",
                table: "EstadoReserva",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_EstadoReserva_EstadoReservaId",
                table: "Reserva",
                column: "EstadoReservaId",
                principalTable: "EstadoReserva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
