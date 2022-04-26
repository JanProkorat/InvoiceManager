using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InvoiceManagerMVC.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    xID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoice_name = table.Column<string>(type: "text", unicode: false, nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoice__313C590AED48FA28", x => x.xID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    xID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount_to_pay = table.Column<int>(type: "integer", nullable: false),
                    receiver = table.Column<string>(type: "text", unicode: false, nullable: false),
                    deadline = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    k_invoice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceI__313C590ACEB8958D", x => x.xID);
                    table.ForeignKey(
                        name: "FK__InvoiceIt__k_inv__267ABA7A",
                        column: x => x.k_invoice,
                        principalTable: "Invoice",
                        principalColumn: "xID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_k_invoice",
                table: "InvoiceItem",
                column: "k_invoice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
