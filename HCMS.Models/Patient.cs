using System.ComponentModel.DataAnnotations;

namespace HCMS.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        // Personal Identification Information
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(20)]
        public string NationalIdentificationNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string HospitalIdentificationNumber { get; set; }

        [StringLength(20)]
        public string MaritalStatus { get; set; }

        public byte[] Photograph { get; set; }

        // Contact Information
        [Required]
        [StringLength(100)]
        public string HomeAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [Phone]
        public string HomePhone { get; set; }

        [Phone]
        public string MobilePhone { get; set; }

        [Phone]
        public string WorkPhone { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        // Emergency Contact Information
        [Required]
        [StringLength(50)]
        public string EmergencyContactName { get; set; }

        [Required]
        [StringLength(50)]
        public string EmergencyContactRelation { get; set; }

        [Required]
        [Phone]
        public string EmergencyContactPhone { get; set; }

        [StringLength(100)]
        public string EmergencyContactAddress { get; set; }

        // Demographic Information
        [StringLength(50)]
        public string Race { get; set; }

        [StringLength(50)]
        public string Ethnicity { get; set; }

        [StringLength(50)]
        public string PrimaryLanguage { get; set; }

        [StringLength(50)]
        public string Occupation { get; set; }

        [StringLength(50)]
        public string EmploymentStatus { get; set; }

        // Insurance Information
        [StringLength(50)]
        public string InsuranceProvider { get; set; }

        [StringLength(50)]
        public string PolicyNumber { get; set; }

        [StringLength(50)]
        public string GroupNumber { get; set; }

        [StringLength(50)]
        public string PolicyholderName { get; set; }

        public string CoverageDetails { get; set; }

        // Medical History
        public string PastMedicalHistory { get; set; }

        public string FamilyMedicalHistory { get; set; }

        public List<string> Allergies { get; set; }

        public List<string> ImmunizationRecords { get; set; }

        public List<string> CurrentMedications { get; set; }

        public string SocialHistory { get; set; }

        public string ObstetricHistory { get; set; }

        // Clinical Data
        public string ChiefComplaint { get; set; }

        public List<VitalSign> VitalSigns { get; set; }

        public List<string> PhysicalExaminationFindings { get; set; }

        public List<string> Diagnoses { get; set; }

        public List<TreatmentPlan> TreatmentPlans { get; set; }

        public List<ProgressNote> ProgressNotes { get; set; }

        public List<LabResult> LabResults { get; set; }

        public List<Procedure> Procedures { get; set; }

        public List<Consultation> Consultations { get; set; }

        public string DischargeSummary { get; set; }

        // Administrative Information
        [Required]
        public DateTime AdmissionDate { get; set; }

        [StringLength(50)]
        public string AdmittingDepartment { get; set; }

        [StringLength(10)]
        public string BedNumber { get; set; }

        [StringLength(50)]
        public string AttendingPhysician { get; set; }

        public string ReferringPhysician { get; set; }

        public List<LegalDocument> LegalDocuments { get; set; }

        public List<BillingRecord> BillingInformation { get; set; }

        public List<Appointment> AppointmentHistory { get; set; }

        // Behavioral Health Information
        public string MentalHealthHistory { get; set; }

        public string CurrentMentalHealthStatus { get; set; }

        public List<PsychosocialAssessment> PsychosocialAssessments { get; set; }

        // Genomic Information
        public List<GeneticTestResult> GeneticTestingResults { get; set; }

        public List<PersonalizedMedicinePlan> PersonalizedMedicinePlans { get; set; }

        // Lifestyle Information
        public string DietaryHabits { get; set; }

        public string ExerciseAndPhysicalActivity { get; set; }

        // Patient Preferences
        public string CommunicationPreferences { get; set; }

        public string CulturalPreferences { get; set; }

        public string EndOfLifePreferences { get; set; }

        // Research Participation
        public List<ClinicalTrial> ClinicalTrials { get; set; }

        public List<ResearchConsent> ConsentForDataUse { get; set; }

        // Constructor to initialize lists
        public Patient()
        {
            Allergies = new List<string>();
            ImmunizationRecords = new List<string>();
            CurrentMedications = new List<string>();
            VitalSigns = new List<VitalSign>();
            PhysicalExaminationFindings = new List<string>();
            Diagnoses = new List<string>();
            TreatmentPlans = new List<TreatmentPlan>();
            ProgressNotes = new List<ProgressNote>();
            LabResults = new List<LabResult>();
            Procedures = new List<Procedure>();
            Consultations = new List<Consultation>();
            LegalDocuments = new List<LegalDocument>();
            BillingInformation = new List<BillingRecord>();
            AppointmentHistory = new List<Appointment>();
            PsychosocialAssessments = new List<PsychosocialAssessment>();
            GeneticTestingResults = new List<GeneticTestResult>();
            PersonalizedMedicinePlans = new List<PersonalizedMedicinePlan>();
            ClinicalTrials = new List<ClinicalTrial>();
            ConsentForDataUse = new List<ResearchConsent>();
        }
    }

    // Supporting classes for complex types
    public class VitalSign
    {
        public DateTime Date { get; set; }
        public double BloodPressure { get; set; }
        public double HeartRate { get; set; }
        public double Temperature { get; set; }
        public double RespiratoryRate { get; set; }
        public double OxygenSaturation { get; set; }
    }

    public class TreatmentPlan
    {
        public DateTime Date { get; set; }
        public string PlanDetails { get; set; }
        public string Goals { get; set; }
        public string ExpectedOutcomes { get; set; }
    }

    public class ProgressNote
    {
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }

    public class LabResult
    {
        public DateTime Date { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
        public string NormalRange { get; set; }
    }

    public class Procedure
    {
        public DateTime Date { get; set; }
        public string ProcedureName { get; set; }
        public string Description { get; set; }
    }

    public class Consultation
    {
        public DateTime Date { get; set; }
        public string SpecialistName { get; set; }
        public string Notes { get; set; }
    }

    public class LegalDocument
    {
        public string DocumentType { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }

    public class BillingRecord
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
    }

    public class Appointment
    {
        public DateTime Date { get; set; }
        public string Department { get; set; }
        public string Doctor { get; set; }
        public string Reason { get; set; }
    }

    public class PsychosocialAssessment
    {
        public DateTime Date { get; set; }
        public string Assessment { get; set; }
    }

    public class GeneticTestResult
    {
        public DateTime Date { get; set; }
        public string TestName { get; set; }
        public string Result { get; set; }
    }

    public class PersonalizedMedicinePlan
    {
        public DateTime Date { get; set; }
        public string PlanDetails { get; set; }
    }

    public class ClinicalTrial
    {
        public DateTime Date { get; set; }
        public string TrialName { get; set; }
        public string Description { get; set; }
    }

    public class ResearchConsent
    {
        public DateTime Date { get; set; }
        public string ConsentDetails { get; set; }
    }
}
