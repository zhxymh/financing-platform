using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_FinancialProduct_22030710544043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFinancialProducts_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppFinancialProducts_FinancialProductId",
                table: "AppFinancialProducts");

            migrationBuilder.RenameColumn(
                name: "FinancialProductId",
                table: "AppFinancialProducts",
                newName: "LastModifierId");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CreditCeiling",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "APR",
                table: "AppFinancialProducts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppFinancialProducts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppFinancialProducts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppFinancialProducts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppFinancialProducts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppFinancialProducts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppFinancialProducts",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppFinancialProducts");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppFinancialProducts");

            migrationBuilder.RenameColumn(
                name: "LastModifierId",
                table: "AppFinancialProducts",
                newName: "FinancialProductId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "AppFinancialProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditCeiling",
                table: "AppFinancialProducts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "APR",
                table: "AppFinancialProducts",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
    }
}
