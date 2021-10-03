using Microsoft.EntityFrameworkCore.Migrations;

namespace Deployer.API.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HostName",
                table: "Targets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SshPassword",
                table: "Targets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SshPort",
                table: "Targets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SshUser",
                table: "Targets",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostName",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "SshPassword",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "SshPort",
                table: "Targets");

            migrationBuilder.DropColumn(
                name: "SshUser",
                table: "Targets");
        }
    }
}
