using Microsoft.EntityFrameworkCore.Migrations;

namespace PiHealth.Migrations
{
    public partial class ChangeinTemplatePrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TemplatePrescription");

            migrationBuilder.DropColumn(
                name: "MedicinName",
                table: "TemplatePrescription");

            migrationBuilder.AddColumn<string>(
                name: "MedicineName",
                table: "TemplatePrescription",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "TemplatePrescription",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicineName",
                table: "TemplatePrescription");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "TemplatePrescription");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TemplatePrescription",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicinName",
                table: "TemplatePrescription",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
