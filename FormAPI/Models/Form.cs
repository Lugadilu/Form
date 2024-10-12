using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.Models
{
    public class Form
    {
        //[JsonIgnore]//to hide id in requests it uses newtonsoft.json
        // public int Id { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Form Name is required.")]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        
        public string Name { get; set; }

        [StringLength(64, ErrorMessage = "Description must not exceed 64 characters.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Pages cannot be empty.")]
        public ICollection<Page> Pages { get; set; } = new List<Page>();
        public ICollection<FormRecord> FormRecords { get; set; } = new List<FormRecord>();

    }
}
