using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.DatabaseModel.Migrations
{
    public partial class fourth5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_TargetRoles_TargetRoleId",
                table: "Targets");

            migrationBuilder.DropIndex(
                name: "IX_Targets_TargetRoleId",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "TargetRoleId",
                table: "Targets");

            migrationBuilder.CreateTable(
                name: "TargetTargetRole",
                columns: table => new
                {
                    TargetRolesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TargetsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetTargetRole", x => new { x.TargetRolesId, x.TargetsId });
                    table.ForeignKey(
                        name: "FK_TargetTargetRole_TargetRoles_TargetRolesId",
                        column: x => x.TargetRolesId,
                        principalTable: "TargetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetTargetRole_Targets_TargetsId",
                        column: x => x.TargetsId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TargetTargetRole_TargetsId",
                table: "TargetTargetRole",
                column: "TargetsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetTargetRole");

            migrationBuilder.AddColumn<int>(
                name: "TargetRoleId",
                table: "Targets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
    }
}
