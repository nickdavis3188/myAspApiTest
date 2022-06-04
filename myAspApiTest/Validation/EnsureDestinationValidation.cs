using myAspApiTest.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace myAspApiTest.Validation
{
    public class EnsureDestinationValidation:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var tickets = validationContext.ObjectInstance as TicketsModel;

            if (tickets != null && !string.IsNullOrWhiteSpace(tickets.ticketowner))
            {
                if (string.IsNullOrWhiteSpace(tickets.destination))
                    return new ValidationResult("Destination Field is required when the ticketOwner has a value");
            }
            return ValidationResult.Success;
        }
    }
}
