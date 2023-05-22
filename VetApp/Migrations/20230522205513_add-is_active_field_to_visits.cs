using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetApp.Migrations
{
    /// <inheritdoc />
    public partial class addis_active_field_to_visits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "Visit",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "Visit");
        }
    }
}
