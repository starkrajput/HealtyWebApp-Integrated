using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HCMS.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        // Personal Identification Information
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string NationalIdentificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string HospitalIdentificationNumber { get; set; }

        [StringLength(20)]
        public string MaritalStatus { get; set; }

        public byte[] Photograph { get; set; }

        // Contact Information
        [Required]
        [StringLength(100)]
        public string HomeAddress { get; set; }

        [Required]
        [Phone]
        public string HomePhone { get; set; }

        [Phone]
        public string MobilePhone { get; set; }

        [Phone]
        public string WorkPhone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Emergency Contact
        [Required]
        [StringLength(50)]
        public string EmergencyContactName { get; set; }

        [Required]
        [StringLength(50)]
        public string EmergencyContactRelation { get; set; }

        [Required]
        [Phone]
        public string EmergencyContactPhone { get; set; }

        // Professional Information
        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicalLicenseNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseState { get; set; }

        [Required]
        [StringLength(100)]
        public string MedicalSchool { get; set; }

        [StringLength(100)]
        public string ResidencyProgram { get; set; }

        [StringLength(100)]
        public string FellowshipProgram { get; set; }

        [StringLength(100)]
        public string BoardCertifications { get; set; }

        [Required]
        [Range(0, 100)]
        public int YearsOfExperience { get; set; }

        [StringLength(100)]
        public string HospitalAffiliation { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(20)]
        public string OfficeRoomNumber { get; set; }

        [StringLength(100)]
        public string ProfessionalMemberships { get; set; }

        [StringLength(200)]
        public string ResearchInterests { get; set; }

        // Clinical Information
        [Range(0, int.MaxValue)]
        public int PatientLoad { get; set; }

        public List<string> PatientHistories { get; set; }

        public List<string> TreatmentSpecialties { get; set; }

        public List<string> ConsultationNotes { get; set; }

        public List<string> SurgicalHistory { get; set; }

        public List<string> ClinicalTrials { get; set; }

        // Administrative Information
        [Required]
        [StringLength(20)]
        public string EmploymentStatus { get; set; }

        public string ShiftSchedule { get; set; }

        public string VacationSchedule { get; set; }

        public string BillingInformation { get; set; }

        public List<string> PerformanceReviews { get; set; }

        public List<string> LegalDocuments { get; set; }

        // Insurance Information
        [StringLength(100)]
        public string MalpracticeInsuranceProvider { get; set; }

        [StringLength(50)]
        public string MalpracticeInsurancePolicyNumber { get; set; }

        public string InsuranceCoverageDetails { get; set; }

        // Educational Information
        public List<string> ContinuingMedicalEducation { get; set; }

        public List<string> AdditionalCertifications { get; set; }

        // Behavioral Health Information
        public List<string> MentalHealthAssessments { get; set; }

        public string WorkplaceStress { get; set; }

        // Genomic Information
        public List<string> GeneticTestingResults { get; set; }

        public string PersonalizedMedicine { get; set; }

        // Lifestyle Information
        public string DietaryHabits { get; set; }

        public string ExerciseAndPhysicalActivity { get; set; }

        // Preferences and Interests
        public string CommunicationPreferences { get; set; }

        public string CulturalPreferences { get; set; }

       
        // Technology and Equipment Proficiency
        public List<string> MedicalDevices { get; set; }

        public List<string> SoftwareSystems { get; set; }

        // Awards and Recognitions
        public List<string> ProfessionalAwards { get; set; }

        public List<string> Publications { get; set; }

        // Volunteer and Community Work
        public List<string> CommunityService { get; set; }

        public List<string> GlobalHealthInitiatives { get; set; }

        // Constructor to initialize lists
        public Doctor()
        {
            PatientHistories = new List<string>();
            TreatmentSpecialties = new List<string>();
            ConsultationNotes = new List<string>();
            SurgicalHistory = new List<string>();
            ClinicalTrials = new List<string>();
            PerformanceReviews = new List<string>();
            LegalDocuments = new List<string>();
            ContinuingMedicalEducation = new List<string>();
            AdditionalCertifications = new List<string>();
            MentalHealthAssessments = new List<string>();
            GeneticTestingResults = new List<string>();
            MedicalDevices = new List<string>();
            SoftwareSystems = new List<string>();
            ProfessionalAwards = new List<string>();
            Publications = new List<string>();
            CommunityService = new List<string>();
            GlobalHealthInitiatives = new List<string>();
        }
    }
}
