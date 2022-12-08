using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class ImgUrkArticleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e8912fc-c9d6-46a9-94ce-47f03b302e0b", "AQAAAAEAACcQAAAAEBqccnJYtpqG4jf3bFiD6p4TRyoJywOSSr871TYil5dBqRhkoDH2lUghU66Zia7dfg==", "7e827fb9-250d-43ae-943a-d5f4fbe13aa5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcb83c21-beb7-446b-9557-5c88618a8c04", "AQAAAAEAACcQAAAAELT/KmNL9aO4+Y+I/oHQS8Ihps9wHECc45MfprjiQhyUIHQkBDSFe5M+Hm1uD7+9jg==", "00d89607-5aba-44c5-9fb1-e9f77cdc12ad" });
        }
    }
}
