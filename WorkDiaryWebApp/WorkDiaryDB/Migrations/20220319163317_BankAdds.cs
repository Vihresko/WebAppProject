using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkDiaryWebApp.Migrations
{
    public partial class BankAdds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Incomes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ReportedMoney",
                table: "Banks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TakenMoney",
                table: "Banks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "ReportedMoney",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "TakenMoney",
                table: "Banks");
        }
    }
}
