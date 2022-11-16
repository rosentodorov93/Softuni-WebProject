using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedWorkoutAndExerciseTampleteTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Workouts");

            migrationBuilder.CreateTable(
                name: "WorkoutTamplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTamplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutTamplates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTamplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    BodyPart = table.Column<int>(type: "int", nullable: false),
                    SetCount = table.Column<int>(type: "int", nullable: false),
                    WorkoutTamplateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTamplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseTamplates_WorkoutTamplates_WorkoutTamplateId",
                        column: x => x.WorkoutTamplateId,
                        principalTable: "WorkoutTamplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTamplates_WorkoutTamplateId",
                table: "ExerciseTamplates",
                column: "WorkoutTamplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTamplates_UserId",
                table: "WorkoutTamplates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTamplates");

            migrationBuilder.DropTable(
                name: "WorkoutTamplates");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Workouts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_ApplicationUserId",
                table: "Workouts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
