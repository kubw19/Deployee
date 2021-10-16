using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.DatabaseModel.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetRoleId",
                table: "Targets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TargetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Targets_TargetRoleId",
                table: "Targets",
                column: "TargetRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_TargetRoles_TargetRoleId",
                table: "Targets",
                column: "TargetRoleId",
                principalTable: "TargetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_TargetRoles_TargetRoleId",
                table: "Targets");

            migrationBuilder.DropTable(
                name: "TargetRoles");

            migrationBuilder.DropIndex(
                name: "IX_Targets_TargetRoleId",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "TargetRoleId",
                table: "Targets");
        }
    }
}
