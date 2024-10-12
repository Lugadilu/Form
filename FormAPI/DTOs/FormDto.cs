
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace FormAPI.DTOs
{
    public class FormDto
    {

        //public Guid Id { get; set; }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Form Name is required.")]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        public string  Name { get; set; }

        [StringLength(64, ErrorMessage = "Description must not exceed 64 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Pages cannot be empty.")]
        public ICollection<PageDto> Pages { get; set; }

        
       
    }
}

