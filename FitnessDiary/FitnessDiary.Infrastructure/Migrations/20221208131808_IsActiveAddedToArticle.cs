using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class IsActiveAddedToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Articles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02b52032-ec58-496e-b58e-0533479ff27d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abdfce6e-4c94-4024-a1eb-ac6a485e1d9b", "AQAAAAEAACcQAAAAEP/dlWH2sHs0+ZE3WgvBq+PXApaE77kYMSQxqgynmsbZ6LWZp2RNfb7530SWGgA/wg==", "573b6a51-bb79-4487-b4bf-9f14100eba01" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf28b02f-bcd9-4464-9100-6343cc8ca939",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a0b9f73-86c0-4935-89fd-ee8f1619184a", "AQAAAAEAACcQAAAAEK9RX06LE1JZ99MdmrHxEp3rUGjqEBpkTPzvyGkO+SKmHh/SYZWEPs+80CfNg+Mr7Q==", "adada2c0-e76f-471e-acb0-b366906c6d29" });
        }
    }
}
