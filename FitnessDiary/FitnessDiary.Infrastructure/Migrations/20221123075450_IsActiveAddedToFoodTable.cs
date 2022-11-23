using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class IsActiveAddedToFoodTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Food",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34c7cd13-20af-4628-8e86-f10600d8a3d3", "AQAAAAEAACcQAAAAENrp0FZZTrQLOA0uFiOc+HFGL0Ud0naP7sqVRo544yHM2jZzHZKy1LRsuHXr4xOAZw==", "2e94a8f4-de97-45a9-b02e-48b287cde09d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "031fd27e-e969-4fa8-a24f-0b3e86f0b5fd", "AQAAAAEAACcQAAAAEM4v38Rj/KM5B3fL2rgkP//LRYAs1oQXtv2y9t0MVaWr1Ij5l1aW7RJYxpMHnBGETg==", "8fb401f0-5f76-4d42-86d6-492437aadd80" });

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: "00c51d79-b0a2-44c4-9dfd-cc197f24c3e8",
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: "7bbc16e1-faa6-46ad-90ba-3dc038105ea2",
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: "8070aa93-ea4c-477e-972b-aa3370f2d701",
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Food");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "769d3aae-79cc-443f-868a-f50c712ad40d", "AQAAAAEAACcQAAAAEFnm1VzI1VO62U4pOqov+Yc9rmt717oXaNhG6ONXI2c8u/Eu7JOztgyZsa06IwKcfQ==", "d01abba8-8219-42a0-a989-723e2d32928d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fa20fb4-c83a-4eaa-95f1-5bf19460e9e9", "AQAAAAEAACcQAAAAEGSmEfPedj+VtGT90jdWakl6lNzCLWdK6k3lPi/ikQhQwFWyuBhhXkDx/p0clFghPg==", "2b7b93ed-141b-46ee-96e5-237daa2e7821" });
        }
    }
}
