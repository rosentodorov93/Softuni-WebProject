using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class RecipeTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Foods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServingsSize = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    NutrtionId = table.Column<int>(type: "int", nullable: false),
                    CaloriesPerServing = table.Column<double>(type: "float", nullable: false),
                    isFinished = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Nutritions_NutrtionId",
                        column: x => x.NutrtionId,
                        principalTable: "Nutritions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_NutrtionId",
                table: "Recipes",
                column: "NutrtionId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Recipes_RecipeId",
                table: "Foods",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Recipes_RecipeId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Foods_RecipeId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Foods");
        }
    }
}
