using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArvatoLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Sequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Sequence);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyDate",
                columns: table => new
                {
                    Sequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrencySequence = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDate", x => x.Sequence);
                    table.ForeignKey(
                        name: "FK_CurrencyDate_Currency_CurrencySequence",
                        column: x => x.CurrencySequence,
                        principalTable: "Currency",
                        principalColumn: "Sequence",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                columns: table => new
                {
                    Sequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyDateSequence = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate", x => x.Sequence);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_CurrencyDate_CurrencyDateSequence",
                        column: x => x.CurrencyDateSequence,
                        principalTable: "CurrencyDate",
                        principalColumn: "Sequence",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDate_CurrencySequence",
                table: "CurrencyDate",
                column: "CurrencySequence");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_CurrencyDateSequence",
                table: "CurrencyRate",
                column: "CurrencyDateSequence");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRate");

            migrationBuilder.DropTable(
                name: "CurrencyDate");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
