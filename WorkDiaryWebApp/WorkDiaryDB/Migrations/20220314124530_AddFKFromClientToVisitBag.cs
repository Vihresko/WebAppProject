using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkDiaryWebApp.Migrations
{
    public partial class AddFKFromClientToVisitBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VisitBagId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_VisitBagId",
                table: "Clients",
                column: "VisitBagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_VisitBags_VisitBagId",
                table: "Clients",
                column: "VisitBagId",
                principalTable: "VisitBags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_VisitBags_VisitBagId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_VisitBagId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "VisitBagId",
                table: "Clients");
        }
    }
}
