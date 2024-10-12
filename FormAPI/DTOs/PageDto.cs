using FormAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.DTOs
{
    public class PageDto
    {
        public PageDto()
        {
            Fields = new List<FormFieldDto>(); // Ensure it's initialized
        }
        public Guid FormId { get; set; }
        //public Form? Form { get; set; }

        [Required(ErrorMessage = "Page must have at least one field.")]
        public ICollection<FormFieldDto> Fields { get; set; }
        //public List<FormFieldDto> Fields { get; set; }
    }
}

/*
using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.Models
{
    public class Page
    {


        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FormId { get; set; }

        // [ForeignKey("FormId")]
        public Form? Form { get; set; }

        public ICollection<FormField> FormFields { get; set; } = new List<FormField>();
    }
}
*/