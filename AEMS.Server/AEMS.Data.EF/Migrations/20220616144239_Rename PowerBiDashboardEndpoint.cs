using Microsoft.EntityFrameworkCore.Migrations;

namespace AEMS.Data.EF.Migrations
{
    public partial class RenamePowerBiDashboardEndpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PowerBiDashboarđEndpoint",
                table: "Device",
                newName: "PowerBiDashboardEndpoint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PowerBiDashboardEndpoint",
                table: "Device",
                newName: "PowerBiDashboarđEndpoint");
        }
    }
}
