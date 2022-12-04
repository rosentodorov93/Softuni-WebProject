using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedModeratorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_ActivityLevels_ActivityLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_UserId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_Nutritions_NutritionId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ApplicationUser_ApplicationUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryDays_ApplicationUser_ApplicationUserId",
                table: "DiaryDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_ApplicationUser_UserId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_ApplicationUser_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTamplates_ApplicationUser_UserId",
                table: "WorkoutTamplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "ApplicationUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_UserId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_NutritionId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_NutritionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_ActivityLevelId",
                table: "ApplicationUsers",
                newName: "IX_ApplicationUsers_ActivityLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a651666-0353-4a96-b3eb-d6b78010b6ba", "a0827383-cf6f-4400-bc86-bdaf2dc35765", "Moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d42a64c-df53-4777-b37b-ced3c5534517", "AQAAAAEAACcQAAAAECGrJ24s7dnXNbF+A/q/Na27sCxWC6PZGPa+pW3KXqOh1PvVos7ziS4djyU8J/cV/Q==", "1c83a7f1-180d-496e-ab70-0f3463255490" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dae7f615-6fda-4be7-a360-2fa5116d4c1f", "AQAAAAEAACcQAAAAEE5bEcD/ky6uXMfhRjqioz4/NJ/cRo8D//BA0A2oW8xFGZj+SrAlwJHTEyjWq42aYQ==", "61a8a683-2dcf-442c-b33d-a6343e41c71c" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_ActivityLevels_ActivityLevelId",
                table: "ApplicationUsers",
                column: "ActivityLevelId",
                principalTable: "ActivityLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Nutritions_NutritionId",
                table: "ApplicationUsers",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ApplicationUsers_ApplicationUserId",
                table: "Articles",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryDays_ApplicationUsers_ApplicationUserId",
                table: "DiaryDays",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_ApplicationUsers_UserId",
                table: "Foods",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_ApplicationUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTamplates_ApplicationUsers_UserId",
                table: "WorkoutTamplates",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_ActivityLevels_ActivityLevelId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Nutritions_NutritionId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ApplicationUsers_ApplicationUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryDays_ApplicationUsers_ApplicationUserId",
                table: "DiaryDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_ApplicationUsers_UserId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_ApplicationUsers_UserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutTamplates_ApplicationUsers_UserId",
                table: "WorkoutTamplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsers",
                table: "ApplicationUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a651666-0353-4a96-b3eb-d6b78010b6ba");

            migrationBuilder.RenameTable(
                name: "ApplicationUsers",
                newName: "ApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_UserId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_NutritionId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_NutritionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsers_ActivityLevelId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_ActivityLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0785c73c-ad8c-4d62-ab18-823bd5d73efa", "AQAAAAEAACcQAAAAELoielrI9DC9lYUH/FWOExcCA0SG2cf6nm67VkQJzUZI3JiJlqa5QkWtvFaC69TXHw==", "55c7b8f0-b2d6-430b-9fdf-4500eab2699b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cdfb08e-e552-4571-8d6d-1816ef8f0409", "AQAAAAEAACcQAAAAEDJLRd2MrlbPLl04kUxUUVNoqNvMeThYuMOWF6mYSROgdcJbp+M6xKlpCB5onU1nwg==", "7329d6b7-703c-471b-a1ae-64ac3ade0369" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_ActivityLevels_ActivityLevelId",
                table: "ApplicationUser",
                column: "ActivityLevelId",
                principalTable: "ActivityLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_UserId",
                table: "ApplicationUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_Nutritions_NutritionId",
                table: "ApplicationUser",
                column: "NutritionId",
                principalTable: "Nutritions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ApplicationUser_ApplicationUserId",
                table: "Articles",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryDays_ApplicationUser_ApplicationUserId",
                table: "DiaryDays",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_ApplicationUser_UserId",
                table: "Foods",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_ApplicationUser_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutTamplates_ApplicationUser_UserId",
                table: "WorkoutTamplates",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
