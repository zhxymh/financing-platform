using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_Apply_22030813300911 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnterpriceName",
                table: "AppApplies",
                newName: "EnterpriseName");

            migrationBuilder.AlterColumn<int>(
                name: "ApplyStatus",
                table: "AppApplies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnterpriseName",
                table: "AppApplies",
                newName: "EnterpriceName");

            migrationBuilder.AlterColumn<string>(
                name: "ApplyStatus",
                table: "AppApplies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
