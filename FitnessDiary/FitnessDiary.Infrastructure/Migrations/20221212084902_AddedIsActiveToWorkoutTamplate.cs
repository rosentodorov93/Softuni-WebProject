using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedIsActiveToWorkoutTamplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WorkoutTamplates",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8dfb20fc-5fcc-44ac-889e-1cb51aae45bf", "AQAAAAEAACcQAAAAECPlNtHzm0OSrBgAaelrs0C/O+v73Zc22lMkqpG8v/aZVy0PEPK328B9Ko5oCtzNHw==", "fd68a1ca-ac02-4e49-9aed-4ef67f33abc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51fbda83-3567-43d1-9ea2-11cbac86de17", "AQAAAAEAACcQAAAAEKOe7OYtaQZRQpRknLcegEnadoZmBwLsOjSyEX46iq05kk0L2OF0C+8KDcHDOUankw==", "f173fc14-2ddf-4763-af51-01ed0ae41323" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WorkoutTamplates");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cb9bf1d-6794-4370-ba1c-001a0d994ef4", "AQAAAAEAACcQAAAAEPt5vm6z6HXut8CeSVFTa8de662CznzRCsChhSG49RLI8wLQ6g06LJPSxuaF7PUorg==", "084e3a5b-f61c-425b-9930-c7b1e4c1dbcd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb583043-c23e-463a-ba14-c8f9fdcc39e3", "AQAAAAEAACcQAAAAENJj8UEXJtYVe7Rr9tIk4txw2rRLJSiL6E9ePtA17OOyspJThZgg+dFZhuKm8wbALQ==", "eaf4b05a-69c2-4135-afdd-1657e29d0e16" });
        }
    }
}
