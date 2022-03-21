using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tank.Financing.Migrations
{
    public partial class Updated_EnterpriseDetail_22032115244341 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "commitUserName",
                table: "AppEnterpriseDetails",
                newName: "CommitUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommitUserName",
                table: "AppEnterpriseDetails",
                newName: "commitUserName");
        }
    }
}
