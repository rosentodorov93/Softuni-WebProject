using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class UserRoleAddedToPlamiUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "312a798827de4362920a10e2a2b12e0c", "9d6a8aea-aae9-44d4-ae4c-89f1236a96c4" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "312a798827de4362920a10e2a2b12e0c", "9d6a8aea-aae9-44d4-ae4c-89f1236a96c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a88da865-bdc2-4afe-9f4f-1447e95222be", "AQAAAAEAACcQAAAAEE4eP2+axVITQwHFR3q2/9tPk5aE06NgB+vJSe8Ge0sDb0xrsL8AUgVDjgLPZDsUrg==", "09c67791-a49f-463b-b717-6f1cb08b234e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f5ffae7-a62a-44e4-8bd4-9ffddb1e8c6d", "AQAAAAEAACcQAAAAEPK+hFyL3nt4RUV8DqJIcCsIUOS5nC0Dvs+JgzclTRmoKnN52On1FVMi3jDp67kM3Q==", "cac12043-032b-41fa-b8fa-7c6fec381e76" });
        }
    }
}
