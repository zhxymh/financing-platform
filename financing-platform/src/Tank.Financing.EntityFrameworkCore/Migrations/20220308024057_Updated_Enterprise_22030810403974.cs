using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_Enterprise_22030810403974 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateStatus",
                table: "AppEnterprises",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateStatus",
                table: "AppEnterprises");
        }
    }
}
