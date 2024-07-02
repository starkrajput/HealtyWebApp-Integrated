using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HCMS.Models
{
    public class Admission
    {
        public int AdmissionId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        [Required]
        [StringLength(50)]
        public string BedNumber { get; set; }

        [Required]
        public int AttendingDoctorId { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForAdmission { get; set; }

        [StringLength(1000)]
        public string AdmissionNotes { get; set; }

        public DateTime? DischargeDate { get; set; }

        [StringLength(1000)]
        public string DischargeNotes { get; set; }

        public bool IsDischarged { get; set; }
        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient Patient { get; set; }
        [ForeignKey("AttendingDoctorId")]
        [ValidateNever]
        public virtual Doctor AttendingDoctor { get; set; }
    }
}
