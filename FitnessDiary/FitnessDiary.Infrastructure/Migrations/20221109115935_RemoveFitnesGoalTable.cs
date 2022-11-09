using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class RemoveFitnesGoalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessGoals");

            migrationBuilder.AlterColumn<int>(
                name: "FitnessGoal",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FitnessGoal",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                table: "FitnessGoals",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Lose weight" });

            migrationBuilder.InsertData(
                table: "FitnessGoals",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Gain weight" });

            migrationBuilder.InsertData(
                table: "FitnessGoals",
                columns: new[] { "Id", "Type" },
                values: new object[] { 3, "Maintain weight" });
        }
    }
}
