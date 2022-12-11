using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class SeedArticleCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ArticleCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "351a06c6-9c12-45d4-9cd1-c9ff5db75212", "Lifestyle" },
                    { "c4763ddf-d44c-41e2-b2eb-2d9885cddcd0", "Nutrition" },
                    { "dd454378-f80d-4380-b6fd-ff592b4aca4d", "Training and Tecniques" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: "351a06c6-9c12-45d4-9cd1-c9ff5db75212");

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: "c4763ddf-d44c-41e2-b2eb-2d9885cddcd0");

            migrationBuilder.DeleteData(
                table: "ArticleCategories",
                keyColumn: "Id",
                keyValue: "dd454378-f80d-4380-b6fd-ff592b4aca4d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80c6fa7f-e852-47ed-b1b1-ff72dc9c17d4", "AQAAAAEAACcQAAAAEKicUCQm4ScU2G2liPnHtf6McVWvzpjq4ZbK32RRkEpUSE085q/9XaRl1UXwHGVOGA==", "e9dc2a17-fd7c-495a-84ba-ef76b2f2cc43" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67704411-3a88-4e29-8ae2-39dee8842984", "AQAAAAEAACcQAAAAEBQVPIkXaZlEC5QD1U8XSDcdTEzxMj8vljiOOAKl994k5fIjVbYvG5yLWTfgKA+1HQ==", "9aadfd4c-346e-445d-bd42-adcf20445202" });
        }
    }
}
