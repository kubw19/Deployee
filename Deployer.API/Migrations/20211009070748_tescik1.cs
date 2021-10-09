using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.API.Migrations
{
    public partial class tescik1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputProperty_DeploySteps_DeployStepId",
                table: "InputProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InputProperty",
                table: "InputProperty");

            migrationBuilder.RenameTable(
                name: "InputProperty",
                newName: "InputProperties");

            migrationBuilder.RenameIndex(
                name: "IX_InputProperty_DeployStepId",
                table: "InputProperties",
                newName: "IX_InputProperties_DeployStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputProperties",
                table: "InputProperties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputProperties_DeploySteps_DeployStepId",
                table: "InputProperties",
                column: "DeployStepId",
                principalTable: "DeploySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputProperties_DeploySteps_DeployStepId",
                table: "InputProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InputProperties",
                table: "InputProperties");

            migrationBuilder.RenameTable(
                name: "InputProperties",
                newName: "InputProperty");

            migrationBuilder.RenameIndex(
                name: "IX_InputProperties_DeployStepId",
                table: "InputProperty",
                newName: "IX_InputProperty_DeployStepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputProperty",
                table: "InputProperty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InputProperty_DeploySteps_DeployStepId",
                table: "InputProperty",
                column: "DeployStepId",
                principalTable: "DeploySteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
