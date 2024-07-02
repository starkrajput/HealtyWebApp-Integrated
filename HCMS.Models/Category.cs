using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCMS.Models
{
    public class Category
    {
        // Annotation to tell this is a primary key also it takes default as priamry key
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Category")]
        // Range Validation with custom message: 
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayCategory { get; set; }
    }
}
