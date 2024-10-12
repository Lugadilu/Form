
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.Models
{
    public class FormRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid FormId { get; set; }
        [JsonIgnore]
        public Form Form { get; set; } // Navigation property

        // JSON string to store form field values
         //public string FormFieldValues { get; set; } = "{}"; //stores a JSON string
        
        
        [Column(TypeName = "jsonb")] //  EF treats this as a JSONB column
                                     
        public string FormFieldValues { get; set; } = "{}";


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public DateTime UpdatedAt { get; set; }
    }
}
