using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.DTOs
{
    public class FormFieldDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid InternalId { get; set; } = Guid.NewGuid();

        // 'name' is required and follows the pattern for alphabetic characters
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name must only contain alphabetic characters.")]
        public string Name { get; set; } // HTML name attribute

        // 'id' is required and follows the pattern for alphabetic characters
        [Required(ErrorMessage = "Field Id is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Id must only contain alphabetic characters.")]
        public string Id { get; set; } // HTML id attribute

        // 'required' is a boolean, defaulting to false
        public bool Required { get; set; } = false; // Default: false

        // 'attributes' is an object (key-value pairs of additional HTML attributes)
        [Required]
        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        // 'kind' is required, and its value is "profile" in this case
        [Required]
        // public string Kind { get; set; } // e.g., "profile"
        public string Kind { get; set; }
        public string FieldType { get; set; }

        // 'fieldType' is required, and its value is "profileId" in this case
        // [Required]
        // public string FieldType { get; set; } // e.g., "profileId"

        // 'rules' is an object (key-value pairs of validation rules)
       // [Required]
        public Dictionary<string, object> Rules { get; set; } = new Dictionary<string, object>();

      
    }

    /// <summary>
    /// Enum to define valid kinds for a FormField.
    /// </summary>
    public enum FieldKind
    {
        Profile,
        Text,
        Date,
        Select,
        Checkbox,
        Radio,
        Spacer,
        SubForm,
        Html
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
        AddressType,
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


/*
using System.Text.Json.Serialization;
namespace FormAPI.DTOs
{
    public class FormFieldDto
    {
       // public Guid? InternalId { get; set; }
        public string Name { get; set; }
        //[JsonIgnore]

        public string Id { get; set; }
        public bool Required { get; set; }
        // public string Attributes { get; set; }
        public object Attributes { get; set; } = new { };
        public string Kind { get; set; }
        public string FieldType { get; set; }
        // public Dictionary<string, string> Rules { get; set; }
        public object Rules { get; set; } = new { };
       
        //public int FormId { get; set; }


    }
}
*/