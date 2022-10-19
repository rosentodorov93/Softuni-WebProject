using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class FitnessLeveltableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FitnessGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessGoals", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ActivityLevels",
                columns: new[] { "Id", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "Low", 1.1 },
                    { 2, "Medium", 1.2 },
                    { 3, "High", 1.3 },
                    { 4, "Very High", 1.4 }
                });

            migrationBuilder.InsertData(
                table: "FitnessGoals",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Lose weight" },
                    { 2, "Gain weight" },
                    { 3, "Maintain weight" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessGoals");

            migrationBuilder.DeleteData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
