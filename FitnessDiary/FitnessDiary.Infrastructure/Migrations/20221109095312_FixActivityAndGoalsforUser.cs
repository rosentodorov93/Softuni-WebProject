using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class FixActivityAndGoalsforUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ActivityLevels_ActivityLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FitnessGoals_FitnessGoalId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FitnessGoalId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "FitnessGoalId",
                table: "AspNetUsers",
                newName: "ProteinPercent");

            migrationBuilder.RenameColumn(
                name: "ActivityLevelId",
                table: "AspNetUsers",
                newName: "NutritionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ActivityLevelId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_NutritionId");

            migrationBuilder.AddColumn<double>(
                name: "ActivityLevel",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CarbsPercent",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FatsPercent",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FitnessGoal",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nutritions_NutritionId",
                table: "AspNetUsers",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nutritions_NutritionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActivityLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarbsPercent",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FatsPercent",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FitnessGoal",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ProteinPercent",
                table: "AspNetUsers",
                newName: "FitnessGoalId");

            migrationBuilder.RenameColumn(
                name: "NutritionId",
                table: "AspNetUsers",
                newName: "ActivityLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_NutritionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ActivityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FitnessGoalId",
                table: "AspNetUsers",
                column: "FitnessGoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ActivityLevels_ActivityLevelId",
                table: "AspNetUsers",
                column: "ActivityLevelId",
                principalTable: "ActivityLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FitnessGoals_FitnessGoalId",
                table: "AspNetUsers",
                column: "FitnessGoalId",
                principalTable: "FitnessGoals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
