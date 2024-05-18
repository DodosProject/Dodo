using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoDo.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, "admin@admin.com", "Admin", "admin", new DateTime(2024, 4, 3, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "ignaciocasaus1cns@gmail.com", "Ignacio", "patata", new DateTime(2024, 4, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "emaildealex@gmail.com", "Alex", "pimiento", new DateTime(2024, 4, 5, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "pepe@pepe.com", "Pepe", "pepe", new DateTime(2024, 4, 15, 19, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "DoTasks",
                columns: new[] { "Id", "Completed", "CreationDate", "Description", "Priority", "Title", "UserId" },
                values: new object[,]
                {
                    { 10001, false, new DateTime(2024, 5, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "Tengo que fregar el suelo", 1, "Fregar", 1 },
                    { 10002, false, new DateTime(2024, 5, 16, 19, 30, 0, 0, DateTimeKind.Unspecified), "Hacer la compra de comida del mes", 1, "Comprar", 1 },
                    { 10003, true, new DateTime(2024, 4, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "Ir al banco a ingresar dinero", 2, "Banco", 3 },
                    { 10004, false, new DateTime(2024, 4, 24, 16, 30, 0, 0, DateTimeKind.Unspecified), "Leer 3 capítulos de La comunidad del anillo", 1, "Leer", 2 },
                    { 10005, true, new DateTime(2024, 4, 28, 19, 30, 0, 0, DateTimeKind.Unspecified), "Hacer la colada de la ropa sucia", 3, "Colada", 4 },
                    { 10006, true, new DateTime(2024, 5, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), "Crear una App en Vue contenerizada", 4, "Crear App", 3 },
                    { 10007, true, new DateTime(2024, 4, 11, 19, 30, 0, 0, DateTimeKind.Unspecified), "Clonar el repositorio que me han mandado", 2, "Clonar repo", 3 },
                    { 10008, false, new DateTime(2024, 5, 9, 19, 30, 0, 0, DateTimeKind.Unspecified), "Desplegar la App en AWS", 1, "Desplegar", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoTasks_UserId",
                table: "DoTasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoTasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
