using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_FinancialProduct_22030315241502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FinancialProductId",
                table: "AppFinancialProducts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts",
                column: "FinancialProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFinancialProducts_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts",
                column: "FinancialProductId",
                principalTable: "AppFinancialProducts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFinancialProducts_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "FinancialProductId",
                table: "AppFinancialProducts");
        }
    }
}
