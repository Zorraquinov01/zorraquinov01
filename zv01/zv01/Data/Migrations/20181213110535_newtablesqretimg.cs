using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zv01.Data.Migrations
{
    public partial class newtablesqretimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QRsId",
                table: "Reserva",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImgsId",
                table: "Evento",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventImg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImgUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QRImg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QRUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRImg", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_QRsId",
                table: "Reserva",
                column: "QRsId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_ImgsId",
                table: "Evento",
                column: "ImgsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_EventImg_ImgsId",
                table: "Evento",
                column: "ImgsId",
                principalTable: "EventImg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserva_QRImg_QRsId",
                table: "Reserva",
                column: "QRsId",
                principalTable: "QRImg",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_EventImg_ImgsId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserva_QRImg_QRsId",
                table: "Reserva");

            migrationBuilder.DropTable(
                name: "EventImg");

            migrationBuilder.DropTable(
                name: "QRImg");

            migrationBuilder.DropIndex(
                name: "IX_Reserva_QRsId",
                table: "Reserva");

            migrationBuilder.DropIndex(
                name: "IX_Evento_ImgsId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "QRsId",
                table: "Reserva");

            migrationBuilder.DropColumn(
                name: "ImgsId",
                table: "Evento");
        }
    }
}
