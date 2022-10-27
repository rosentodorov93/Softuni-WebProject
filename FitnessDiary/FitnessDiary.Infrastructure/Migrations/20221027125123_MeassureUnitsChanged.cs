using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class MeassureUnitsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessureUnits",
                table: "Foods");

            migrationBuilder.AddColumn<int>(
                name: "MeassureUnits",
                table: "Foods",
                type: "int",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeassureUnits",
                table: "Foods");

            migrationBuilder.AddColumn<string>(
                name: "MessureUnits",
                table: "Foods",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
