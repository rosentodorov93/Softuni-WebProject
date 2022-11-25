using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class RemoveUnitsColumnFromRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ea0dcbc-717f-4ef3-9108-acf1530e10ac", "AQAAAAEAACcQAAAAEACy96Hr0ygJOwkrG7UVtd2CqlIoeAiXZzOmOs8zKqcg87KhzLWyL5bx43Aiv5USLw==", "a981b4e1-66a9-4f7d-b815-607102dc5976" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "519aecfb-38e7-4da4-910f-c23c9c2074e8", "AQAAAAEAACcQAAAAEIpI3/2wmYRZzuf93yItB8haGv/NbldA0CgP2+YwdpXgI6HZhwXCTVEoG8vJWHp4FA==", "8859e1a3-9f87-4414-870b-e6a7a0566602" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
