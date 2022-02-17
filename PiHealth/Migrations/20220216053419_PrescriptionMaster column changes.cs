using Microsoft.EntityFrameworkCore.Migrations;

namespace PiHealth.Migrations
{
    public partial class PrescriptionMastercolumnchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PrescriptionMaster");

            migrationBuilder.DropColumn(
                name: "MedicinName",
                table: "PrescriptionMaster");

            migrationBuilder.AddColumn<string>(
                name: "MedicineName",
                table: "PrescriptionMaster",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PrescriptionMaster",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineName",
                table: "PrescriptionMaster");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PrescriptionMaster");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PrescriptionMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicinName",
                table: "PrescriptionMaster",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
