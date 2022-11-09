using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class RemoveDiaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Diaries_DiaryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DiaryDays_Diaries_DiaryId",
                table: "DiaryDays");

            migrationBuilder.DropTable(
                name: "Diaries");

            migrationBuilder.DropIndex(
                name: "IX_DiaryDays_DiaryId",
                table: "DiaryDays");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiaryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiaryId",
                table: "DiaryDays");

            migrationBuilder.DropColumn(
                name: "DiaryId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DiaryDays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Light ", 1.375 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Moderate ", 1.55 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Very Active", 1.7250000000000001 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Extra Active", 1.8999999999999999 });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDays_ApplicationUserId",
                table: "DiaryDays",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryDays_AspNetUsers_ApplicationUserId",
                table: "DiaryDays",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaryDays_AspNetUsers_ApplicationUserId",
                table: "DiaryDays");

            migrationBuilder.DropIndex(
                name: "IX_DiaryDays_ApplicationUserId",
                table: "DiaryDays");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DiaryDays");

            migrationBuilder.AddColumn<int>(
                name: "DiaryId",
                table: "DiaryDays",
                type: "int",
                nullable: true);

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
                    table.ForeignKey(
                        name: "FK_Diaries_DiaryDays_DiaryDayId",
                        column: x => x.DiaryDayId,
                        principalTable: "DiaryDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Low", 1.1000000000000001 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Medium", 1.2 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Type", "Value" },
                values: new object[] { "High", 1.3 });

            migrationBuilder.UpdateData(
                table: "ActivityLevels",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Type", "Value" },
                values: new object[] { "Very High", 1.3999999999999999 });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDays_DiaryId",
                table: "DiaryDays",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiaryId",
                table: "AspNetUsers",
                column: "DiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Diaries_DiaryDayId",
                table: "Diaries",
                column: "DiaryDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Diaries_DiaryId",
                table: "AspNetUsers",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryDays_Diaries_DiaryId",
                table: "DiaryDays",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id");
        }
    }
}
