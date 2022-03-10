using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_Apply_22031013504482 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplyTxId",
                table: "AppApplies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ApproveAllowanceTxId",
                table: "AppApplies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OfflineApproveTxId",
                table: "AppApplies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OnlineApproveTxId",
                table: "AppApplies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SetAllowanceTxId",
                table: "AppApplies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyTxId",
                table: "AppApplies");

            migrationBuilder.DropColumn(
                name: "ApproveAllowanceTxId",
                table: "AppApplies");

            migrationBuilder.DropColumn(
                name: "OfflineApproveTxId",
                table: "AppApplies");

            migrationBuilder.DropColumn(
                name: "OnlineApproveTxId",
                table: "AppApplies");

            migrationBuilder.DropColumn(
                name: "SetAllowanceTxId",
                table: "AppApplies");
        }
    }
}
