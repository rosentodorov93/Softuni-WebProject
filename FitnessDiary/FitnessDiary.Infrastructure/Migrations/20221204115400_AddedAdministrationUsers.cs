using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedAdministrationUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "2aa628cc-ef0a-47fe-b7ce-05981113826b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "312a798827de4362920a10e2a2b12e0c", "9d6a8aea-aae9-44d4-ae4c-89f1236a96c4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b");

            migrationBuilder.CreateTable(
                name: "AdministrationUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrationUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02b52032-ec58-496e-b58e-0533479ff27d", 0, "abdfce6e-4c94-4024-a1eb-ac6a485e1d9b", "moderator@mail.bg", false, false, null, "MODERATOR@MAIL.BG", "MODERATOR", "AQAAAAEAACcQAAAAEP/dlWH2sHs0+ZE3WgvBq+PXApaE77kYMSQxqgynmsbZ6LWZp2RNfb7530SWGgA/wg==", null, false, "573b6a51-bb79-4487-b4bf-9f14100eba01", false, "moderator" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cf28b02f-bcd9-4464-9100-6343cc8ca939", 0, "9a0b9f73-86c0-4935-89fd-ee8f1619184a", "admin@mail.bg", false, false, null, "ADMIN@MAIL.BG", "ADMIN", "AQAAAAEAACcQAAAAEK9RX06LE1JZ99MdmrHxEp3rUGjqEBpkTPzvyGkO+SKmHh/SYZWEPs+80CfNg+Mr7Q==", null, false, "adada2c0-e76f-471e-acb0-b366906c6d29", false, "admin" });

            migrationBuilder.InsertData(
                table: "AdministrationUsers",
                columns: new[] { "Id", "FirstName", "LastName", "UserId" },
                values: new object[,]
                {
                    { "066f7491-cf3e-481a-9afd-9a4e8e276a50", "Moderator", "Moderatorov", "02b52032-ec58-496e-b58e-0533479ff27d" },
                    { "0c68b5c9-40f7-44de-b03b-0afe35157e35", "Admin", "Adminov", "cf28b02f-bcd9-4464-9100-6343cc8ca939" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6a651666-0353-4a96-b3eb-d6b78010b6ba", "02b52032-ec58-496e-b58e-0533479ff27d" },
                    { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "cf28b02f-bcd9-4464-9100-6343cc8ca939" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministrationUsers_UserId",
                table: "AdministrationUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministrationUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6a651666-0353-4a96-b3eb-d6b78010b6ba", "02b52032-ec58-496e-b58e-0533479ff27d" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "cf28b02f-bcd9-4464-9100-6343cc8ca939" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "312a798827de4362920a10e2a2b12e0c", "9d6a8aea-aae9-44d4-ae4c-89f1236a96c4" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22f4a16f-9f78-4823-a2f4-50bf48eed431", 0, "1d42a64c-df53-4777-b37b-ced3c5534517", "guest@mail.bg", false, false, null, "GUEST@MAIL.BG", "GUEST", "AQAAAAEAACcQAAAAECGrJ24s7dnXNbF+A/q/Na27sCxWC6PZGPa+pW3KXqOh1PvVos7ziS4djyU8J/cV/Q==", null, false, "1c83a7f1-180d-496e-ab70-0f3463255490", false, "guest" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2aa628cc-ef0a-47fe-b7ce-05981113826b", 0, "dae7f615-6fda-4be7-a360-2fa5116d4c1f", "admin@mail.bg", false, false, null, "ADMIN@MAIL.BG", "ADMIN", "AQAAAAEAACcQAAAAEE5bEcD/ky6uXMfhRjqioz4/NJ/cRo8D//BA0A2oW8xFGZj+SrAlwJHTEyjWq42aYQ==", null, false, "61a8a683-2dcf-442c-b33d-a6343e41c71c", false, "admin" });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "ActivityLevelId", "Age", "CarbsPercent", "FatsPercent", "FitnessGoal", "FullName", "Gender", "Height", "NutritionId", "ProteinPercent", "UserId", "Weight" },
                values: new object[] { "fc8c8e1e-b196-41e4-a733-7adcd4509634", 1, 29, 50, 30, 3, "Pesho Petrov", 1, 178, 1, 20, "22f4a16f-9f78-4823-a2f4-50bf48eed431", 80.0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "2aa628cc-ef0a-47fe-b7ce-05981113826b" });
        }
    }
}
