using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class AddPetCares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetCares",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CaretakerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetCares_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetCares_Volunteers_CaretakerId",
                        column: x => x.CaretakerId,
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetCares_CaretakerId",
                table: "PetCares",
                column: "CaretakerId");

            migrationBuilder.CreateIndex(
                name: "IX_PetCares_PetId",
                table: "PetCares",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetCares");
        }
    }
}
