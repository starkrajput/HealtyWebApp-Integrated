using HCMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HCMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        // Basic configuration of entity framework.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
                   }
        // Everything with database happen here because it is dbcontext.
        // Now Categories Table will be created in db.[Entity Framweok Era]
        public DbSet<Category> Categories { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Product> Products { get; set; }
        // add-migration AddCategorizTableToDb
        // then to certify these migrations -> update-database
        public DbSet<MailAccounts> MailAccounts { get; set; }
        public DbSet<EmailTemplates> EmailTemplates { get; set; }
        public DbSet<IGTemplates> IGTemplates { get; set; }
        public DbSet<IGAccounts> IGAccounts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Medication> Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1 ,   Name="Action", DisplayCategory = 1},
                new Category { Id = 2, Name = "Sci", DisplayCategory = 1 },
                new Category { Id = 3, Name = "His", DisplayCategory = 1 }
                );
			modelBuilder.Entity<MailAccounts>().HasData(
				 new MailAccounts { Id = 1, Email = "devingainsrsv@gmail.com", Password = "vjtg drmk mmhm logq", DefaultMessageTemplate = "Fill This !", DefaultSubject = "Default Subject" });
            modelBuilder.Entity<IGAccounts>().HasData(new IGAccounts{ Id=1,Username="rsvtechnologis",Password="HeyGoodness@668800",DefaultMessageTemplate="Message",DefaultSubject="DeSubject"});
            modelBuilder.Entity<IGTemplates>().HasData(new IGTemplates {Id = 1, Content = "Hi this ", Subject = "Subject Line", TemplateName = "Temp1"});
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Admission>().ToTable("Admission");
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = 1,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Specialization = "Cardiology",
                    PhoneNumber = "alice.johnson@example.com",
                    Email = "alice.johnson@example.com",
                    Address = "789 Maple St",
                    City = "Springfield",
                    State = "IL",
                    Country = "USA",
                    PostalCode = "62704",
                    DateOfBirth = new DateTime(1975, 5, 12),
                    DateOfJoining = new DateTime(2000, 6, 1),
                    MedicalLicenseNumber = "LIC123456",
                    Qualifications = "MD, Cardiology",
                    Experience = "20 years",
                });
            modelBuilder.Entity<Admission>().HasData(
               new Admission
               {
                   AdmissionId = 1,
                   PatientId = 1,
                   AdmissionDate = DateTime.Now.AddDays(-10),
                   Department = "Cardiology",
                   BedNumber = "C123",
                   AttendingDoctorId = 1,
                   ReasonForAdmission = "Chest Pain",
                   AdmissionNotes = "Patient admitted for evaluation of chest pain.",
                   DischargeDate = DateTime.Now.AddDays(-2),
                   DischargeNotes = "Discharged after successful treatment.",
                   IsDischarged = true
               });
           modelBuilder.Entity<Medication>().HasData(
                new Medication
                {
                    MedicationId = 1,
                    PatientId = 1,
                    MedicationName = "Lisinopril",
                    Dosage = "10mg",
                    StartDate = DateTime.Now.AddMonths(-1),
                    EndDate = DateTime.Now.AddMonths(5)
                });
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Age = 45,
                    Gender = "Male",
                    ContactDetails = "123-456-7890",
                    Email = "john.doe@example.com",
                    Address = "123 Main St",
                    City = "Springfield",
                    State = "IL",
                    Country = "USA",
                    PostalCode = "62704",
                    EmergencyContactName = "Jane Doe",
                    EmergencyContactRelation = "Wife",
                    EmergencyContactPhone = "098-765-4321",
                    MedicalHistory = "No major illnesses",
                    Allergies = "Peanuts",
                    CurrentMedications = "None",
                    Diseases = new List<string> { "Hypertension" },
                    PrimaryPhysician = "Dr. Smith",
                    InsuranceProvider = "HealthFirst",
                    InsurancePolicyNumber = "HF123456",
                    AdmissionDate = DateTime.Now.AddDays(-10),
                    Department = "Cardiology",
                    BedNumber = "C123",
                    AttendingDoctor = "Dr. Adams"
                });
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.AttendingDoctor)
                .WithMany(d => d.Admissions)
                .HasForeignKey(a => a.AttendingDoctorId);
            modelBuilder.Entity<Product>().HasData(
			   new Product { Id = 1, 
                   Title = "Fortune of Time",
                   Author = "Billy Spark" ,
                   Description = "Present Vitae sodales libero.",
                   ISBN ="SWD9999001",
                   ListPrice =99,
                   Price =90,
                   Price50 = 85,
                   Price100 =80,
				   CategoryId = 1,
				   ImageUrl = ""
               },
			   new Product
			   {
				   Id = 2,
				   Title = "Fortune of Times",
				   Author = "Billy Spark",
				   Description = "Present Vitae sodales libero.",
				   ISBN = "SWD9999001",
				   ListPrice = 99,
				   Price = 90,
				   Price50 = 85,
				   Price100 = 80,
				   CategoryId = 2,
				   ImageUrl = ""
			   },
				 new Product
				 {
					 Id = 3,
					 Title = "Fortune of Times",
					 Author = "Billy Spark",
					 Description = "Present Vitae sodales libero.",
					 ISBN = "SWD9999001",
					 ListPrice = 99,
					 Price = 95,
					 Price50 = 85,
					 Price100 = 80,
					 CategoryId = 3,
					 ImageUrl = ""
				 }
			   );
		}
    }
}

//	bootswatch 
