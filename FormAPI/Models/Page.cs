using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Models
{
    public class Page
    {

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FormId { get; set; }

       // [ForeignKey("FormId")]
        public Form? Form { get; set; }

        [Required(ErrorMessage = "Page must have at least one field.")]
        public ICollection<FormField> FormFields { get; set; } = new List<FormField>();
    }
}
