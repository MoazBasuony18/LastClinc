using Microsoft.EntityFrameworkCore.Migrations;

namespace LastClinc.Migrations
{
    public partial class intitiDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complain",
                table: "PatientDataForDoctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Exmination",
                table: "PatientDataForDoctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Investigations",
                table: "PatientDataForDoctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PastHistory",
                table: "PatientDataForDoctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "PatientDataForDoctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complain",
                table: "PatientDataForDoctors");

            migrationBuilder.DropColumn(
                name: "Exmination",
                table: "PatientDataForDoctors");

            migrationBuilder.DropColumn(
                name: "Investigations",
                table: "PatientDataForDoctors");

            migrationBuilder.DropColumn(
                name: "PastHistory",
                table: "PatientDataForDoctors");

            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "PatientDataForDoctors");
        }
    }
}
