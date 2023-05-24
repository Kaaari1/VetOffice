using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Presen",
                table: "Presen");

            migrationBuilder.DropColumn(
                name: "work_hours",
                table: "Presen");

            migrationBuilder.AddColumn<int>(
                name: "dayoff_id",
                table: "Presen",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "Presen",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "work_hours_from",
                table: "Doctor",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "work_hours_to",
                table: "Doctor",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presen",
                table: "Presen",
                column: "dayoff_id");

            migrationBuilder.CreateIndex(
                name: "IX_Presen_id_doctor",
                table: "Presen",
                column: "id_doctor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Presen",
                table: "Presen");

            migrationBuilder.DropIndex(
                name: "IX_Presen_id_doctor",
                table: "Presen");

            migrationBuilder.DropColumn(
                name: "dayoff_id",
                table: "Presen");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "Presen");

            migrationBuilder.DropColumn(
                name: "work_hours_from",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "work_hours_to",
                table: "Doctor");

            migrationBuilder.AddColumn<DateTime>(
                name: "work_hours",
                table: "Presen",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Presen",
                table: "Presen",
                column: "id_doctor");
        }
    }
}
