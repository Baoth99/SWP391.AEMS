using Microsoft.EntityFrameworkCore.Migrations;

namespace AEMS.Data.EF.Migrations
{
    public partial class AddIdinEquipmentLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentLog",
                table: "EquipmentLog");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "EquipmentLog",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentLog",
                table: "EquipmentLog",
                columns: new[] { "DeviceId", "EventTime", "CreatedAt", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentLog",
                table: "EquipmentLog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EquipmentLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentLog",
                table: "EquipmentLog",
                columns: new[] { "DeviceId", "EventTime", "CreatedAt" });
        }
    }
}
