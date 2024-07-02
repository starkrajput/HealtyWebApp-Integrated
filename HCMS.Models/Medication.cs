

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCMS.Models
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient Patient { get; set; }
    }

  




}
