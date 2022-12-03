using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "312a798827de4362920a10e2a2b12e0c", "beb7ec18c2d24b65b6c4e2cfaf0bb03b", "User", "USER" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "312a798827de4362920a10e2a2b12e0c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "218afb74-27f7-49ed-9e98-4159e844f68a", "AQAAAAEAACcQAAAAEM4jTxwaQZHGStp9x19Rv6ejWuWadye6cbrxypHd2Pv6+37Q9gSCKYrjZ7LmguxoEA==", "c7cf6b30-5af7-4718-ba26-5fbb9ac7f598" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7300cb5f-ef3b-4823-a000-1cbfb8ee1f0a", "AQAAAAEAACcQAAAAENgRnxTnqmCKCifBwAlU+I9JLqMD/wrfSaudj+tq6lnnJZAAwjvTuY62/kSp/slh9A==", "6b152035-88e7-4780-baaf-ff409b2363f8" });
        }
    }
}
