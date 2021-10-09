using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.API.Migrations
{
    public partial class tescik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Options",
                table: "DeploySteps");

            migrationBuilder.CreateTable(
                name: "InputProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    DeployStepId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InputProperty_DeploySteps_DeployStepId",
                        column: x => x.DeployStepId,
                        principalTable: "DeploySteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InputProperty_DeployStepId",
                table: "InputProperty",
                column: "DeployStepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputProperty");

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "DeploySteps",
                type: "TEXT",
                nullable: true);
        }
    }
}
