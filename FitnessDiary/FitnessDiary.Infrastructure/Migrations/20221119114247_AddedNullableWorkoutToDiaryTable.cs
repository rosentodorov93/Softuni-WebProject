﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessDiary.Infrastructure.Data.Migrations
{
    public partial class AddedNullableWorkoutToDiaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkoutId",
                table: "DiaryDays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaryDays_WorkoutId",
                table: "DiaryDays",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaryDays_Workouts_WorkoutId",
                table: "DiaryDays",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaryDays_Workouts_WorkoutId",
                table: "DiaryDays");

            migrationBuilder.DropIndex(
                name: "IX_DiaryDays_WorkoutId",
                table: "DiaryDays");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "DiaryDays");
        }
    }
}
