using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_FinancialProduct_22032115204608 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "features",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "url_logo1",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "url_logo2",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "url_logo3",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "url_logo4",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "url_logo5",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "features",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "url_logo1",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "url_logo2",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "url_logo3",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "url_logo4",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "url_logo5",
                table: "AppFinancialProducts");
        }
    }
}
