using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.DatabaseModel.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "schReleases",
                table: "Releases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Releases_ProjectId",
                schema: "schReleases",
                table: "Releases",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Releases_Projects_ProjectId",
                schema: "schReleases",
                table: "Releases",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Releases_Projects_ProjectId",
                schema: "schReleases",
                table: "Releases");

            migrationBuilder.DropIndex(
                name: "IX_Releases_ProjectId",
                schema: "schReleases",
                table: "Releases");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "schReleases",
                table: "Releases");
        }
    }
}
