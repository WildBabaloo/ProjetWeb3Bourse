using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetWeb3Bourse.Migrations
{
    /// <inheritdoc />
    public partial class DoubleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bourseId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    heure = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valeur = table.Column<double>(type: "float", nullable: false),
                    variation = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Evenement_Bourse_bourseId",
                        column: x => x.bourseId,
                        principalTable: "Bourse",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_bourseId",
                table: "Evenement",
                column: "bourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evenement");
        }
    }
}
