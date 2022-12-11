using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedArticleCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ApplicationUsers_ApplicationUserId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ApplicationUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ArticleCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategories", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleCategories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "ArticleCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleCategories_CategoryId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleCategories");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0828f358-c481-47c4-a4c3-1fac8493886e", "AQAAAAEAACcQAAAAEDlJlM1fxMAVQIRPEB9tO+Gh8CjobS0UI9jbnZmxjynLQP75Au6+wyNHdVOmV25T1Q==", "f8cb22dc-3206-450b-8b69-918b78ea7846" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0264ded-1df2-48c2-8742-d0cf0d1da30f", "AQAAAAEAACcQAAAAENe9y5oCVzpDEmT4m77s7WX1hNrF4Zf0rmNIqu0RiFJ8gq+bkKG1emZj9VDHYFnyYg==", "4dcc2d14-98dd-4118-8299-6790bc8ce25b" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ApplicationUserId",
                table: "Articles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ApplicationUsers_ApplicationUserId",
                table: "Articles",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }
    }
}
