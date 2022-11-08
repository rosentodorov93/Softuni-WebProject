using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class DiaryTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiaryId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Diaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaryDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NutritionId = table.Column<int>(type: "int", nullable: false),
                    DiaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryDays_Diaries_DiaryId",
                        column: x => x.DiaryId,
                        principalTable: "Diaries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiaryDays_Nutritions_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "Nutritions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    NutritionId = table.Column<int>(type: "int", nullable: false),
                    DiaryDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servings_DiaryDays_DiaryDayId",
                        column: x => x.DiaryDayId,
                        principalTable: "DiaryDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servings_Nutritions_NutritionId",
                        column: x => x.NutritionId,
                        principalTable: "Nutritions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiaryId",
                table: "AspNetUsers",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_DiaryDayId",
                table: "Diaries",
                column: "DiaryDayId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDays_DiaryId",
                table: "DiaryDays",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDays_NutritionId",
                table: "DiaryDays",
                column: "NutritionId");

            migrationBuilder.CreateIndex(
                name: "IX_Servings_DiaryDayId",
                table: "Servings",
                column: "DiaryDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Servings_NutritionId",
                table: "Servings",
                column: "NutritionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Diaries_DiaryId",
                table: "AspNetUsers",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_DiaryDays_DiaryDayId",
                table: "Diaries",
                column: "DiaryDayId",
                principalTable: "DiaryDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Diaries_DiaryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_DiaryDays_DiaryDayId",
                table: "Diaries");

            migrationBuilder.DropTable(
                name: "Servings");

            migrationBuilder.DropTable(
                name: "DiaryDays");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiaryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiaryId",
                table: "AspNetUsers");
        }
    }
}
