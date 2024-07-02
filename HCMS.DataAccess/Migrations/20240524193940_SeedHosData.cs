using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedHosData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "DoctorId", "Address", "City", "Country", "DateOfBirth", "DateOfJoining", "Email", "Experience", "FirstName", "LastName", "MedicalLicenseNumber", "PhoneNumber", "PostalCode", "Qualifications", "Specialization", "State" },
                values: new object[] { 1, "789 Maple St", "Springfield", "USA", new DateTime(1975, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.johnson@example.com", "20 years", "Alice", "Johnson", "LIC123456", "alice.johnson@example.com", "62704", "MD, Cardiology", "Cardiology", "IL" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "PatientId", "Address", "AdmissionDate", "Age", "Allergies", "AttendingDoctor", "BedNumber", "City", "ContactDetails", "Country", "CurrentMedications", "Department", "Diseases", "Email", "EmergencyContactName", "EmergencyContactPhone", "EmergencyContactRelation", "FirstName", "Gender", "InsurancePolicyNumber", "InsuranceProvider", "LastName", "MedicalHistory", "PostalCode", "PrimaryPhysician", "State" },
                values: new object[] { 1, "123 Main St", new DateTime(2024, 5, 14, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(2001), 45, "Peanuts", "Dr. Adams", "C123", "Springfield", "123-456-7890", "USA", "None", "Cardiology", "[\"Hypertension\"]", "john.doe@example.com", "Jane Doe", "098-765-4321", "Wife", "John", "Male", "HF123456", "HealthFirst", "Doe", "No major illnesses", "62704", "Dr. Smith", "IL" });

            migrationBuilder.InsertData(
                table: "Admission",
                columns: new[] { "AdmissionId", "AdmissionDate", "AdmissionNotes", "AttendingDoctorId", "BedNumber", "Department", "DischargeDate", "DischargeNotes", "IsDischarged", "PatientId", "ReasonForAdmission" },
                values: new object[] { 1, new DateTime(2024, 5, 14, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1855), "Patient admitted for evaluation of chest pain.", 1, "C123", "Cardiology", new DateTime(2024, 5, 22, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1913), "Discharged after successful treatment.", true, 1, "Chest Pain" });

            migrationBuilder.InsertData(
                table: "Medications",
                columns: new[] { "MedicationId", "Dosage", "EndDate", "MedicationName", "PatientId", "StartDate" },
                values: new object[] { 1, "10mg", new DateTime(2024, 10, 24, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1959), "Lisinopril", 1, new DateTime(2024, 4, 24, 22, 39, 37, 382, DateTimeKind.Local).AddTicks(1949) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admission",
                keyColumn: "AdmissionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medications",
                keyColumn: "MedicationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "DoctorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "PatientId",
                keyValue: 1);
        }
    }
}
