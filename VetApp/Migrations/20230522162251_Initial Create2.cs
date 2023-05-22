using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "name_a",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "nameands",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "surname",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Users_Logins");

            migrationBuilder.DropColumn(
                name: "nameands",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "age",
                table: "Animal");

            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "Doctor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "dateofbirth",
                table: "Animal",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_id_user",
                table: "Doctor",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_User_id_user",
                table: "Doctor",
                column: "id_user",
                principalTable: "User",
                principalColumn: "id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_User_id_user",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_id_user",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "dateofbirth",
                table: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Visit",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_a",
                table: "Visit",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nameands",
                table: "Visit",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                table: "Visit",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Users_Logins",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nameands",
                table: "Doctor",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "age",
                table: "Animal",
                type: "TEXT",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }
    }
}
