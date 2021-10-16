using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.DatabaseModel.Migrations
{
    public partial class fourth6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TargetRoles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Default role" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TargetRoles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
