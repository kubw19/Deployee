using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.DatabaseModel.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "schReleases");

            migrationBuilder.CreateTable(
                name: "Releases",
                schema: "schReleases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseArtifacts",
                schema: "schReleases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtifactId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseArtifacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseArtifacts_ArtifactVersions_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "ArtifactVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseArtifacts_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalSchema: "schReleases",
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseArtifacts_ArtifactId",
                schema: "schReleases",
                table: "ReleaseArtifacts",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseArtifacts_ReleaseId",
                schema: "schReleases",
                table: "ReleaseArtifacts",
                column: "ReleaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseArtifacts",
                schema: "schReleases");

            migrationBuilder.DropTable(
                name: "Releases",
                schema: "schReleases");
        }
    }
}
