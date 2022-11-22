using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class SeedAdminRoleToAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "a685f0cf-6d50-49b1-99cb-dbb987c7f62e" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd1439f9-201b-42ac-96d2-5f13fd35ad5a", "a685f0cf-6d50-49b1-99cb-dbb987c7f62e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a685f0cf-6d50-49b1-99cb-dbb987c7f62e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2d3a676-b92c-4ff9-b5d7-fa864184c5c3", "AQAAAAEAACcQAAAAELkbN5zgkflWNj/hfbqoQWxfL6oUJRlHItJztnu1L+WqEePV2S+0SSz7hTlQMxdmDg==", "76c3c5a2-c724-4ceb-a9ce-663958728772" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc8c8e1e-b196-41e4-a733-7adcd4509634",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cef17dc2-5d28-430f-b592-84296601f143", "AQAAAAEAACcQAAAAEKDb9s6h0taylqkYAlyU6dSdEayQrioidxT9Y8d3+4hmMrApYXAnmyThtS4v416rKQ==", "77a1f34f-c201-4b51-9fea-f64fd4349d2e" });
        }
    }
}
