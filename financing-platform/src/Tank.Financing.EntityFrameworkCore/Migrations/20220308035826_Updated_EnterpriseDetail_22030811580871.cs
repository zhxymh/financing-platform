using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_EnterpriseDetail_22030811580871 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StaffNumber",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StaffNumber",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
