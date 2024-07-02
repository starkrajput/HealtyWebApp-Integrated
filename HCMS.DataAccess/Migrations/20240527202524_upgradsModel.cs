using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class upgradsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReasonForAdmission",
                table: "Admission",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DischargeNotes",
                table: "Admission",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Admission",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BedNumber",
                table: "Admission",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdmissionNotes",
                table: "Admission",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Admission",
                keyColumn: "AdmissionId",
                keyValue: 1,
                columns: new[] { "AdmissionDate", "DischargeDate" },
                values: new object[] { new DateTime(2024, 5, 17, 23, 25, 21, 108, DateTimeKind.Local).AddTicks(976), new DateTime(2024, 5, 25, 23, 25, 21, 108, DateTimeKind.Local).AddTicks(1033) });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "MedicationId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 27, 23, 25, 21, 108, DateTimeKind.Local).AddTicks(1073), new DateTime(2024, 4, 27, 23, 25, 21, 108, DateTimeKind.Local).AddTicks(1068) });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 1,
                column: "AdmissionDate",
                value: new DateTime(2024, 5, 17, 23, 25, 21, 108, DateTimeKind.Local).AddTicks(1105));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReasonForAdmission",
                table: "Admission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "DischargeNotes",
                table: "Admission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Admission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BedNumber",
                table: "Admission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "AdmissionNotes",
                table: "Admission",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "Admission",
                keyColumn: "AdmissionId",
                keyValue: 1,
                columns: new[] { "AdmissionDate", "DischargeDate" },
                values: new object[] { new DateTime(2024, 5, 14, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1855), new DateTime(2024, 5, 22, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1913) });

            migrationBuilder.UpdateData(
                table: "Medications",
                keyColumn: "MedicationId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 24, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1959), new DateTime(2024, 4, 24, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1949) });

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 1,
                column: "AdmissionDate",
                value: new DateTime(2024, 5, 14, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(2001));
        }
    }
}
