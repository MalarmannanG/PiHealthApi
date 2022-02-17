using Microsoft.EntityFrameworkCore.Migrations;

namespace PiHealth.Migrations
{
    public partial class DiagonsisMastertableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisMaster_PatientProfile_PatientProfileId",
                table: "DiagnosisMaster");

            migrationBuilder.DropIndex(
                name: "IX_DiagnosisMaster_PatientProfileId",
                table: "DiagnosisMaster");

            migrationBuilder.DropColumn(
                name: "PatientProfileId",
                table: "DiagnosisMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientProfileId",
                table: "DiagnosisMaster",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisMaster_PatientProfileId",
                table: "DiagnosisMaster",
                column: "PatientProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisMaster_PatientProfile_PatientProfileId",
                table: "DiagnosisMaster",
                column: "PatientProfileId",
                principalTable: "PatientProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
