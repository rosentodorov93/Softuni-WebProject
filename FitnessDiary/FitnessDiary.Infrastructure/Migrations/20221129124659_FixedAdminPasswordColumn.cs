using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class FixedAdminPasswordColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22f4a16f-9f78-4823-a2f4-50bf48eed431",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0a142c6-f847-445f-96b9-f3e564571e64", "AQAAAAEAACcQAAAAEHdeWhNVZTZhFtIlYfnjv4q7XC1GII/VmdULKApJMPoVMmiXziSwVsphJH9Cxdi5uw==", "bdf6052e-d7a5-45e0-8ddc-1025e394fa81" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2aa628cc-ef0a-47fe-b7ce-05981113826b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab089084-44e2-4eb2-99e1-5aa44a2ad321", null, "6ae0995e-bdb6-42dc-9c47-35d85fe49892" });
        }
    }
}
