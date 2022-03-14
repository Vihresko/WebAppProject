using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkDiaryWebApp.Migrations
{
    public partial class ReconstructVisitBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientProcedures_VisitBags_VisitBagId",
                table: "ClientProcedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProcedures",
                table: "ClientProcedures");

            migrationBuilder.DropIndex(
                name: "IX_ClientProcedures_VisitBagId",
                table: "ClientProcedures");

            migrationBuilder.AlterColumn<string>(
                name: "VisitBagId",
                table: "ClientProcedures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ClientProcedures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProcedures",
                table: "ClientProcedures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_ClientId",
                table: "ClientProcedures",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientProcedures",
                table: "ClientProcedures");

            migrationBuilder.DropIndex(
                name: "IX_ClientProcedures_ClientId",
                table: "ClientProcedures");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClientProcedures");

            migrationBuilder.AlterColumn<string>(
                name: "VisitBagId",
                table: "ClientProcedures",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientProcedures",
                table: "ClientProcedures",
                columns: new[] { "ClientId", "ProcedureId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProcedures_VisitBagId",
                table: "ClientProcedures",
                column: "VisitBagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientProcedures_VisitBags_VisitBagId",
                table: "ClientProcedures",
                column: "VisitBagId",
                principalTable: "VisitBags",
                principalColumn: "Id");
        }
    }
}
