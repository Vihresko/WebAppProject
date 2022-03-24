using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkDiaryWebApp.WorkDiaryDB.Migrations
{
    public partial class AddReportDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_MainBanks_MainBankId",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameIndex(
                name: "IX_Report_MainBankId",
                table: "Reports",
                newName: "IX_Reports_MainBankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_MainBanks_MainBankId",
                table: "Reports",
                column: "MainBankId",
                principalTable: "MainBanks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_MainBanks_MainBankId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_MainBankId",
                table: "Report",
                newName: "IX_Report_MainBankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_MainBanks_MainBankId",
                table: "Report",
                column: "MainBankId",
                principalTable: "MainBanks",
                principalColumn: "Id");
        }
    }
}
