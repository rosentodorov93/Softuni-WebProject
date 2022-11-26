using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedImageAndIsActiveToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20e3a6b8-7193-47cd-be70-7f996c2e31f3", "AQAAAAEAACcQAAAAEL22nj1UrI6LubJU6gywOhpdQzggC7JumwD4dWyEh9l6+Nat+PqW1SEUaR1/f3dd4A==", "e1782828-5199-49ad-a73c-a027b7ffe29c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "395ecdbb-74bf-4d11-9aa5-498813679bae", "AQAAAAEAACcQAAAAEPgJezvp2xhZMpg1BLyImY0bh0r1CMapz67C3qRPaD/FFMsDZUs6Dfx5JSKovbd0Bg==", "45ab619f-b8d5-4c33-b5b6-150dafb031eb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsActive",
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
    }
}
