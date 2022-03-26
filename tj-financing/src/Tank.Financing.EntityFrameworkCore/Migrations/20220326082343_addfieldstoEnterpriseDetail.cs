using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class addfieldstoEnterpriseDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalAssets",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredAddress",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Income",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CompleteTxId",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessAddress",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompreDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompreScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreditDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreditScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EnvCreditLevel",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EnvCreditScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FinanceDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FinanceScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HasEvaluate",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "HasExGuarant",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HousefundPaidPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncomePaidTaxPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncomePreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IncomeTaxPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InnovateDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "InnovateScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LiabilityPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ManageDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ManageScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MarketDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MarketScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NetprofitPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PaidAssets",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PatentNumber",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfitDes",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProfitPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProfitScore",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredAssets",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SocialsecurityNumber",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoftbindNumber",
                table: "AppEnterpriseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VatPaidPerYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VatShouldpayPreYear",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompreDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "CompreScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "CreditDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "CreditScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "EnvCreditLevel",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "EnvCreditScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "FinanceDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "FinanceScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "HasEvaluate",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "HasExGuarant",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "HousefundPaidPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "IncomePaidTaxPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "IncomePreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "IncomeTaxPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "InnovateDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "InnovateScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "LiabilityPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "ManageDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "ManageScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "MarketDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "MarketScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "NetprofitPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "PaidAssets",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "PatentNumber",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "ProfitDes",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "ProfitPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "ProfitScore",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "RegisteredAssets",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "SocialsecurityNumber",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "SoftbindNumber",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "TaxPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "VatPaidPerYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.DropColumn(
                name: "VatShouldpayPreYear",
                table: "AppEnterpriseDetails");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "TotalAssets",
                keyValue: null,
                column: "TotalAssets",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TotalAssets",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "RegisteredAddress",
                keyValue: null,
                column: "RegisteredAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RegisteredAddress",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "Industry",
                keyValue: null,
                column: "Industry",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "Income",
                keyValue: null,
                column: "Income",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Income",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "CompleteTxId",
                keyValue: null,
                column: "CompleteTxId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CompleteTxId",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AppEnterpriseDetails",
                keyColumn: "BusinessAddress",
                keyValue: null,
                column: "BusinessAddress",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessAddress",
                table: "AppEnterpriseDetails",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
