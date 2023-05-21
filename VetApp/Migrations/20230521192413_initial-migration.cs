using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    id_doctor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nameands = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.id_doctor);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role_name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id_role);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "Presen",
                columns: table => new
                {
                    id_doctor = table.Column<int>(type: "INTEGER", nullable: false),
                    work_hours = table.Column<DateTime>(type: "TEXT", nullable: false),
                    nworking_days = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presen", x => x.id_doctor);
                    table.ForeignKey(
                        name: "FK_Presen_Doctor_id_doctor",
                        column: x => x.id_doctor,
                        principalTable: "Doctor",
                        principalColumn: "id_doctor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    id_animal = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_a = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    age = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    id_user = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.id_animal);
                    table.ForeignKey(
                        name: "FK_Animal_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Logins",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "INTEGER", nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    id_role = table.Column<int>(type: "INTEGER", nullable: false),
                    role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Logins", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_Users_Logins_Role_id_role",
                        column: x => x.id_role,
                        principalTable: "Role",
                        principalColumn: "id_role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Logins_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    id_visit = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_animal = table.Column<int>(type: "INTEGER", nullable: false),
                    name_a = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    id_doctor = table.Column<int>(type: "INTEGER", nullable: false),
                    nameands = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    id_user = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.id_visit);
                    table.ForeignKey(
                        name: "FK_Visit_Animal_id_animal",
                        column: x => x.id_animal,
                        principalTable: "Animal",
                        principalColumn: "id_animal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_Doctor_id_doctor",
                        column: x => x.id_doctor,
                        principalTable: "Doctor",
                        principalColumn: "id_doctor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visit_User_id_user",
                        column: x => x.id_user,
                        principalTable: "User",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_id_user",
                table: "Animal",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Logins_id_role",
                table: "Users_Logins",
                column: "id_role");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_id_animal",
                table: "Visit",
                column: "id_animal");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_id_doctor",
                table: "Visit",
                column: "id_doctor");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_id_user",
                table: "Visit",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Presen");

            migrationBuilder.DropTable(
                name: "Users_Logins");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
