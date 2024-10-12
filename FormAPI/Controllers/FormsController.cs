using AutoMapper;
using FormAPI.DTOs;
using FormAPI.Models;
using FormAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Logging;


namespace FormAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FormsController> _logger;


        public FormsController(IFormRepository formRepository, IMapper mapper, ILogger<FormsController> logger)
        {
            _formRepository = formRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> ListForms()
        {
            try
            {
                var forms = await _formRepository.GetAllFormsAsync();

                var response = new
                {
                    data = forms.Select(f => new
                    {
                        type = "form",
                        id = f.Id,
                        name = f.Name,
                        description = f.Description,
                        pages = f.Pages.Any()
                            ? f.Pages.Select(p => new
                            {
                                fields = p.FormFields.Select(field => new
                                {
                                    name = field.Name,
                                    id = field.Id,
                                    required = field.Required,
                                    attributes = field.Attributes,
                                    kind = field.Kind, // Using string value now
                                    fieldType = field.FieldType, // Using string value now
                                    //rules = field.Rules
                                    rules = field.Rules ?? new Dictionary<string, object> // Include default or empty rules
                                        {
                                            { "maxLength", field.Rules.ContainsKey("maxLength") ? field.Rules["maxLength"] : 0 }, // Add maxLength if missing
                                            { "pattern", field.Rules.ContainsKey("pattern") ? field.Rules["pattern"] : string.Empty },
                                            { "minLength", field.Rules.ContainsKey("minLength") ? field.Rules["minLength"] : 0 }
                                        }
                                })
                            })
                            : new[] // Ensure same structure for pages with no fields
                            {
                new
                {
                    fields = Enumerable.Empty<object>().Select(_ => new
                    {
                        name = string.Empty,
                        id = string.Empty,
                        required = false,
                        attributes = new Dictionary<string, object>(),
                        kind = "Profile", // Default string value
                        fieldType = "Text", // Default string value
                        rules = new Dictionary<string, object>()
                    })
                }
                            }.AsEnumerable()
                    }),
                    links = new { self = "../dictionary" }
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving forms.",
                    details = ex.Message
                });
            }
        }




        
          [HttpPost]
          public async Task<ActionResult> CreateForm([FromBody] FormDto formDto)
          {
              if (formDto == null)
              {
                  return BadRequest(new { error = "Form data is null." });
              }
              // Check if form has at least one page with at least one field
              if (formDto.Pages == null || !formDto.Pages.Any(p => p.Fields != null && p.Fields.Any()))
              {
                  return BadRequest(new { error = "Form must contain at least one page with at least one field." });
              }

              try
              {
                  // Map the DTO to the Form model
                  var form = _mapper.Map<Form>(formDto);

                  // Generate a new GUID for the form ID (since it's managed by the server)
                  form.Id = Guid.NewGuid();

                  // Add the form to the repository
                  await _formRepository.AddFormAsync(form);

                  // Prepare the response (making sure `data` is an object, not an array)
                  var response = new
                  {
                      data = new
                      {
                          type = "form",  // Specify the type as "form"
                          id = form.Id,   // Include the generated form ID
                          name = form.Name,
                          description = form.Description,
                         // rowVersion = Convert.ToBase64String(form.RowVersion),  // Convert RowVersion to Base64
                          pages = form.Pages.Select(page => new
                          {
                              fields = page.FormFields.Select(field => new
                              {
                                  name = field.Name,
                                  id = field.Id,
                                  kind = field.Kind,
                                  fieldType = field.FieldType,
                                  required = field.Required,
                                  attributes = field.Attributes,
                                  rules = field.Rules

                                  // Deserialize attributes and rules from JSON string to object
                                  //attributes = JsonConvert.DeserializeObject<Dictionary<string, object>>(field.Attributes),
                                  //rules = JsonConvert.DeserializeObject<Dictionary<string, object>>(field.Rules)
                              }).ToArray()
                          }).ToArray()
                      },
                      links = new
                      {
                          self = "../dictionary"
                      }
                  };

                  // Use CreatedAtAction to return a 201 Created response
                  return CreatedAtAction(nameof(GetForm), new { formId = form.Id }, response);
              }
              catch (Exception ex)
              {
                  return StatusCode(500, new
                  {
                      error = "An unexpected error occurred while creating the form.",
                      details = ex.Message
                  });
              }
          }
          


        // GET: forms/{formId}
        [HttpGet("{formId}")]
        public async Task<ActionResult> GetForm(Guid formId)
        {
            try
            {
                var form = await _formRepository.GetFormByIdAsync(formId);

                if (form == null)
                {
                    return NotFound(new { error = "Form not found." });
                }

                var formDto = _mapper.Map<FormDto>(form);

                var response = new
                {
                    data = new
                    {
                        //id = formDto.Id,
                        name = formDto.Name,
                        description = formDto.Description,
                       

                    },
                    links = new
                    {
                        self = Url.Action("GetForm", new { formId })
                    },
                    type = "form" 
                };

                return Content(JsonConvert.SerializeObject(response), "application/vnd+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving the form.",
                    details = ex.Message
                });
            }
        }
        

        // DELETE: api/forms/{formId}
        [HttpDelete("{formId}")]
        public async Task<ActionResult> DeleteForm(Guid formId)
        {
            try
            {
                var form = await _formRepository.GetFormByIdAsync(formId);

                if (form == null)
                {
                    return NotFound(new { error = "Form not found." });
                }

                await _formRepository.DeleteFormAsync(form);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while deleting the form.",
                    details = ex.Message
                });
            }
        }

        [HttpPut("{formId}")]
        public async Task<ActionResult> UpdateForm(Guid formId, [FromBody] FormDto formDto)
        {
            if (formDto == null || formId == Guid.Empty)
            {
                _logger.LogWarning("UpdateForm request failed due to null form data or empty formId.");
                return BadRequest(new { error = "Form data is null or form ID is empty." });
            }

            try
            {
                var existingForm = await _formRepository.GetFormByIdAsync(formId);

                if (existingForm == null)
                {
                    _logger.LogWarning("Form with ID {FormId} not found for update.", formId);
                    return NotFound(new { error = "Form not found." });
                }

                // Map the updated fields from the DTO to the existing form
                _mapper.Map(formDto, existingForm);

                // Save changes to the repository
                await _formRepository.UpdateFormAsync(existingForm);

                _logger.LogInformation("Form with ID {FormId} successfully updated.", formId);

                // Return a 204 NoContent response with no response body
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the form with ID {FormId}.", formId);
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while updating the form.",
                    details = ex.Message
                });
            }
        }

       

        // GET: forms/fields
        [HttpGet("fields")]
        public async Task<ActionResult> GetFormFields()
        {
            try
            {
                var formFields = await _formRepository.GetAllFormFieldsAsync();
                var response = new
                {
                    data = formFields.Select(ff => new
                    {
                        type = "formField", // Each field has a "type" as per JSON:API spec
                        id = ff.Id,
                        attributes = new
                        {
                            name = ff.Name,
                            required = ff.Required,
                            attributes = ff.Attributes, // Assuming this is a dictionary or object
                            kind = ff.Kind,
                            fieldType = ff.FieldType,
                            rules = ff.Rules // Assuming this is a dictionary or object
                        }
                    }),
                    links = new { self = "/forms/fields" } 
                };
               

                return Content(JsonConvert.SerializeObject(response), "application/vnd.api+json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An unexpected error occurred while retrieving form fields.",
                    details = ex.Message
                });
            }
        }
    }
}


