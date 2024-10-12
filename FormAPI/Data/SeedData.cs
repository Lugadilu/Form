
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace FormAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            // Create IDs for relationships
            var formId = Guid.NewGuid();  // Single form ID
            var pageId = Guid.NewGuid();  // Single page ID
            var formFieldId = Guid.NewGuid();  // Single form field ID
            var formRecordId = Guid.NewGuid();  // Single form record ID

            // Seed a Form
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    Id = formId,
                    Name = "JKUSA",
                    Description = "A form to collect customer feedback.",
                  //  RowVersion = Guid.NewGuid().ToByteArray()  // RowVersion for concurrency check
                }
            );

            // Seed a Page
            modelBuilder.Entity<Page>().HasData(
                new Page
                {
                    Id = pageId,
                    FormId = formId  // Link to the seeded form
                }
            );

            // Seed a FormField
            modelBuilder.Entity<FormField>().HasData(
                new FormField
                {
                    InternalId = formFieldId,  // Assuming InternalId is the primary key
                    Id = "profileFirstName",
                    Name = "profileFirstName",
                    Required = true,
                    Attributes = {},
                    
                    Kind = "profile",
                   // Kind = FieldKind.Profile,
                    FieldType = "FirstName",
                   // FieldType = FieldType.ProfileId,
                    Rules = {},
                    //Rules = "{\"minLength\": 2, \"maxLength\": 128}",
                    PageId = pageId  // Link to the seeded page
                }
            );

            // Seed a FormRecord
            modelBuilder.Entity<FormRecord>().HasData(
                new FormRecord
                {
                    Id = formRecordId,
                    FormId = formId,  // Link to the seeded form
                    FormFieldValues = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { "profileFirstName", "Nelly" }  // Example form field value
                    }),
                   
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
