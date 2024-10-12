
using AutoMapper;
using FormAPI.DTOs;
using FormAPI.Models;
using FormAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Controllers
{
    //[Route("[controller]")]
    [Route("forms")]
    //[Route("FormRecords")]
    [ApiController]
    public class FormRecordsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;
        

        public FormRecordsController(IFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }
        [HttpGet("{formId}/records")]
        public async Task<ActionResult> ListFormRecords(string formId)
        {
            try
            {

                if (!Guid.TryParse(formId, out Guid parsedFormId))
                {
                    return BadRequest(new { error = "Invalid formId format." });
                }

                // Retrieve all form records from the repository
                var formRecords = await _formRepository.GetAllFormRecordsAsync();

                // Filter records by the provided formId and construct the response
                var records = formRecords
                    .Where(r => r.FormId == parsedFormId)
                    .Select(r =>
                    {
                        // Deserialize the form field values
                        //var formFieldValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(r.FormFieldValues);

                        // Construct the response record
                        return new
                        {
                            type = "formRecord",
                            id = r.Id,
                            formId = r.FormId,
                            //formFieldValues,
                            formFieldValues = r.FormFieldValues, // Use dictionary directly
                            createdAt = r.CreatedAt
                        };
                    })
                    .ToList();  // Explicitly convert to List to ensure 'records' is treated as an array

                // Prepare the response with 'data' as a plain array (no additional properties like $id)
                var response = new
                {
                    data = records,  // This is a plain array, not an object
                    links = new { self = "/form/formRecords" }
                };

                // Return the response in JSON format
                // return Ok(response);


                // Return response using JsonConvert with default serializer settings
                var serializedResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Avoids self-referencing loops
                    PreserveReferencesHandling = PreserveReferencesHandling.None, // Avoids $id attributes
                    Formatting = Formatting.Indented
                });

                // Return the serialized response with correct content type
                return Content(serializedResponse, "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving form records.",
                    details = ex.Message
                });
            }
        }


        // GET: api/formRecords
        [HttpGet("formRecords")]
        public async Task<ActionResult> ListAllFormRecords()
        {
            try
            {
                var formRecords = await _formRepository.GetAllFormRecordsAsync();

                var response = new
                {
                    data = formRecords.Select(r =>
                    {
                        // Deserialize FormFieldValues into a dynamic object
                        //var formFieldValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(r.FormFieldValues);

                        // Construct the response object
                        var record = new
                        {
                            type = "formRecord",
                            id = r.Id,
                            formId = r.FormId,
                            FormFieldValues = r.FormFieldValues

                        };
                       
                        return new
                        {
                            data = record,
                            links = new { self = "../dictionary" }
                        };
                    }),
                    links = new { self = "/form/formRecords" }
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving form records.",
                    details = ex.Message
                });
            }
        }


        [HttpGet("{formId}/records/{recordId}")]
        public async Task<ActionResult> GetFormRecordById(string formId, string recordId)
        {
            try
            {

                if (!Guid.TryParse(formId, out Guid parsedFormId))
                {
                    return BadRequest(new { error = "Invalid formId format." });
                }

                if (!Guid.TryParse(recordId, out Guid parsedRecordId))
                {
                    return BadRequest(new { error = "Invalid recordId format." });
                }


                // Retrieve the specific form record by formId and recordId from the repository
                var formRecord = await _formRepository.GetFormRecordByIdAsync(parsedFormId, parsedRecordId);

                // If no record is found, return 404
                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                // Construct the response object based on the retrieved record
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formRecord.FormFieldValues,
                        createdAt = formRecord.CreatedAt
                    },
                    links = new { self = $"/forms/{formId}/records/{recordId}" }
                };

                // Serialize the response and return it with the correct Content-Type
                var serializedResponse = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    Formatting = Formatting.Indented
                });

                return Content(serializedResponse, "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving the form record.",
                    details = ex.Message
                });
            }
        }



        [HttpPost("{formId}/records")]
        public async Task<ActionResult> CreateFormRecord(string formId, [FromBody] Dictionary<string, object> formFieldValues)
        {
            try
            {

                if (!Guid.TryParse(formId, out Guid parsedFormId))
                {
                    return BadRequest(new { error = "Invalid formId format." });
                }


                // Ensure form field values are provided
                if (formFieldValues == null || !formFieldValues.Any())
                {
                    return BadRequest(new { error = "Form field values are required." });
                }

                // Retrieve the form definition along with the fields by parsedFormId
                var form = await _formRepository.GetFormWithFieldsAsync(parsedFormId);

                if (form == null)
                {
                    return NotFound(new { error = "Form not found." });
                }

                // Convert formFieldValues to ensure all values are of primitive types (string, int, etc.)
                var simplifiedFormFieldValues = SimplifyFormFieldValues(formFieldValues);


                //Convert the formFieldValues to a JSON string to store in the database
                var serializedFormFieldValues = JsonConvert.SerializeObject(simplifiedFormFieldValues);

                // Create a new FormRecord entity
                var formRecord = new FormRecord
                {
                    Id = Guid.NewGuid(),
                    FormId = parsedFormId,
                    FormFieldValues = serializedFormFieldValues,  // store as JSON string
                    //FormFieldValues = formFieldValues, // Assign directly since it is a dictionary
                    CreatedAt = DateTime.UtcNow
                    // createdAt = formRecord.CreatedAt
                };

                // Save the form record to the database
                await _formRepository.AddFormRecordAsync(formRecord);
               

                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        //formId = formRecord.FormId,
                        // Use the original field values submitted, formatted as JSON string
                        //formFieldValues = formattedFormFieldValues,
                        formFieldValues = formRecord.FormFieldValues,
                        // formFieldValues = simplifiedFormFieldValues,
                        createdAt = formRecord.CreatedAt
                    }
                };



                // Return success response with 201 status
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while creating the form record.",
                    details = ex.Message
                });
            }
        }


        [HttpPatch("{formId}/records/{recordId}")]
        public async Task<ActionResult> UpdateFormRecord(string formId, string recordId, [FromBody] Dictionary<string, object> updatedFields)
        {
            try
            {
                // Validate the formId and recordId formats
                if (!Guid.TryParse(formId, out Guid parsedFormId))
                {
                    return BadRequest(new { error = "Invalid formId format." });
                }

                if (!Guid.TryParse(recordId, out Guid parsedRecordId))
                {
                    return BadRequest(new { error = "Invalid recordId format." });
                }

                // Ensure the updated fields are provided
                if (updatedFields == null || !updatedFields.Any())
                {
                    return BadRequest(new { error = "Updated fields are required." });
                }

                // Retrieve the form record by formId and recordId
                var formRecord = await _formRepository.GetFormRecordByIdAsync(parsedFormId, parsedRecordId);

                // If the form record is not found, return 404
                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                // Deserialize existing form field values into a dictionary
                var existingFormFieldValues = JsonConvert.DeserializeObject<Dictionary<string, object>>(formRecord.FormFieldValues);

                // Update only the fields provided in the request body
                foreach (var field in updatedFields)
                {
                    if (existingFormFieldValues.ContainsKey(field.Key))
                    {
                        // Update existing field value
                        existingFormFieldValues[field.Key] = field.Value;
                    }
                    else
                    {
                        // Add new field value
                        existingFormFieldValues.Add(field.Key, field.Value);
                    }
                }

                // Convert the updated field values back to JSON string
                formRecord.FormFieldValues = JsonConvert.SerializeObject(existingFormFieldValues);

                // Update the record's timestamp
               // formRecord.UpdatedAt = DateTime.UtcNow;

                // Save changes to the repository
                await _formRepository.UpdateFormRecordAsync(formRecord);

                // Create response object with the updated record information
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formRecord.FormFieldValues, // Send updated field values
                       // updatedAt = formRecord.UpdatedAt
                    },
                    links = new { self = $"/forms/{formId}/records/{recordId}" }
                };

                // Return the updated record with 200 status
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while updating the form record.",
                    details = ex.Message
                });
            }
        }


        // DELETE: forms/{formId}/records/{recordId}
         [HttpDelete("{formId}/records/{recordId}")]
        //[HttpDelete("urn:uuid:{formId}/records")]
        public async Task<ActionResult> DeleteFormRecord(Guid formId, Guid recordId)
        {
            try
            {
                var formRecord = await _formRepository.GetFormRecordByIdAsync(formId, recordId);

                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                await _formRepository.DeleteFormRecordAsync(formRecord);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while deleting the form record.",
                    details = ex.Message
                });
            }
        }
        

        [HttpPut("FormRecords/{formId}/records/{recordId}")]
        //[HttpPut("{formId}/records/{recordId}")]
        public async Task<ActionResult> UpdateFormRecord(Guid formId, Guid recordId, [FromBody] Dictionary<string, string> formFieldValues)
        {
            try
            {
                // Validate the input
                if (formFieldValues == null || !formFieldValues.Any())
                {
                    return BadRequest(new { error = "Form field values are required." });
                }

                // Retrieve the form record by recordId and formId
                var formRecord = await _formRepository.GetFormRecordByIdAsync(formId, recordId); //recordId, formId
                if (formRecord == null)
                {
                    return NotFound(new { error = "Form record not found." });
                }

                // Retrieve the form definition
                var form = await _formRepository.GetFormWithFieldsAsync(formId);
                if (form == null)
                {
                    return NotFound(new { error = "Form not found." });
                }

                // Validate the provided fields against the allowed fields
                var allowedFields = form.Pages.SelectMany(p => p.FormFields).Select(ff => ff.Name).ToList();
                var invalidFields = formFieldValues.Keys.Except(allowedFields).ToList();
                if (invalidFields.Any())
                {
                    return BadRequest(new { error = "Invalid fields provided.", invalidFields });
                }

                // Update the form record's field values
               // formRecord.FormFieldValues = JsonConvert.SerializeObject(formFieldValues);

                // Save the updated form record using the repository
                await _formRepository.UpdateFormRecordAsync(formRecord);

                // Return the updated form record as a response
                var response = new
                {
                    data = new
                    {
                        type = "formRecord",
                        id = formRecord.Id,
                        formId = formRecord.FormId,
                        formFieldValues = formFieldValues,
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred while updating the form record.", details = ex.Message });
            }
        }
        private Dictionary<string, object> SimplifyFormFieldValues(Dictionary<string, object> originalValues)
        {
            var simplifiedValues = new Dictionary<string, object>();

            foreach (var keyValuePair in originalValues)
            {
                simplifiedValues[keyValuePair.Key] = GetPrimitiveValue(keyValuePair.Value);
            }

            return simplifiedValues;
        }

        private object GetPrimitiveValue(object value)
        {
            // Check if the value is already a primitive type
            if (value is string || value is int || value is bool || value is double || value is decimal)
            {
                return value; // Return as-is
            }

            // Check if the value is a JsonElement or JToken and get the raw value if necessary
            if (value is Newtonsoft.Json.Linq.JToken jToken)
            {
                return jToken.ToObject<object>(); // Convert JToken to its primitive equivalent
            }

            // Add checks for other types as necessary (e.g., JsonElement for System.Text.Json)
            if (value is System.Text.Json.JsonElement jsonElement)
            {
                switch (jsonElement.ValueKind)
                {
                    case System.Text.Json.JsonValueKind.String:
                        return jsonElement.GetString();
                    case System.Text.Json.JsonValueKind.Number:
                        return jsonElement.GetDouble();
                    case System.Text.Json.JsonValueKind.True:
                        return true;
                    case System.Text.Json.JsonValueKind.False:
                        return false;
                    case System.Text.Json.JsonValueKind.Null:
                        return null;
                    default:
                        return jsonElement.ToString(); // For other types, serialize as string
                }
            }

            // Fallback: if we can't simplify, return the string representation of the value
            return value?.ToString() ?? string.Empty;
        }
       
    }
}
