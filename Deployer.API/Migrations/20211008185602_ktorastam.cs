using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.API.Migrations
{
    public partial class ktorastam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeployStep_Projects_ProjectId",
                table: "DeployStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeployStep",
                table: "DeployStep");

            migrationBuilder.RenameTable(
                name: "DeployStep",
                newName: "DeploySteps");

            migrationBuilder.RenameIndex(
                name: "IX_DeployStep_ProjectId",
                table: "DeploySteps",
                newName: "IX_DeploySteps_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeploySteps",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeploySteps",
                table: "DeploySteps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeploySteps_Projects_ProjectId",
                table: "DeploySteps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeploySteps_Projects_ProjectId",
                table: "DeploySteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeploySteps",
                table: "DeploySteps");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeploySteps");

            migrationBuilder.RenameTable(
                name: "DeploySteps",
                newName: "DeployStep");

            migrationBuilder.RenameIndex(
                name: "IX_DeploySteps_ProjectId",
                table: "DeployStep",
                newName: "IX_DeployStep_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeployStep",
                table: "DeployStep",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeployStep_Projects_ProjectId",
                table: "DeployStep",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
