
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class FormField //provides metadata about formfild.describes structure $xtics of each formfield
    {
        //public Guid Id { get; set; } = Guid.NewGuid();
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InternalId { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Field Id is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Id must only contain alphabetic characters.")]
        public string Id { get; set; } // 'id' attribute to match the HTML 'id' property in the API

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name must only contain alphabetic characters.")]
        public string Name { get; set; }

        //[JsonIgnore]//to hide id in requests it uses newtonsoft.json
        //public int Id { get; set; }

        // [JsonIgnore]
        //public int FormId { get; set; }  // Foreign key to the Form entity

        public bool Required { get; set; } = false; // Default: false // Indicates whether the form field is required for submission.

        [Required]
        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        //public string Attributes { get; set; }// Additional attributes or metadata associated with the form field.
        [Required]
        // public string Kind { get; set; }// Represents the category or type of the form field (e.g., "profile", "address", "html").
        // [Required]
        // public string FieldType { get; set; } //Specifies the type of data expected for the form field (e.g., "text", "email", "checkbox").
        //public Dictionary<string, string> Rules { get; set; }//Defines validation rules or constraints specific to the form field (e.g., minimum/maximum length, regex pattern
        public string Kind { get; set; }


       // [Required(ErrorMessage = "Field Type is required.")]
        public string FieldType { get; set; }

       // [Required]
        public Dictionary<string, object> Rules { get; set; } = new Dictionary<string, object>();

        public Guid PageId { get; set; }
        public Page Page { get; set; }

    }
    /// <summary>
    /// Enum to define valid kinds for a FormField.
    /// </summary>
    public enum FieldKind
    {
        Profile,
        Address,
        Html,
        Registration,
        Extra
    }

    /// <summary>
    /// Enum to define valid field types for a FormField.
    /// </summary>
    public enum FieldType
    {
        ProfileId,
        FirstName,
        MiddleName,
        LastName,
        BirthDate,
        Gender,
        LanguageCode,
        Nationality,
        PhoneNumber,
        Email,
        Arrival,
        Departure,
        AddressId,
        AddressLine,
       //AddressType,
        Zip,
        City,
        Country,
        Label,
        Button,
        Submit,
        Text,
        Textarea,
        Select,
        Checkbox,
        Radio,
        Ordinal,
        Signature,
        Spacer,
        SubForm,
        Html
    }
}

