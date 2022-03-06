using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkDiaryDB.Migrations
{
    public partial class ActiveStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Procedures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");
        }
    }
}
