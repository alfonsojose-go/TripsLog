using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripsLog.Models
{
    public class Trip : IValidatableObject
    {
        public int TripId { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int AccommodationId { get; set; }

        // Accommodation is now optional
        public Accommodation? Accommodation { get; set; } = new Accommodation();


        public int TodoId { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();

        // Custom validation to ensure EndDate is not before StartDate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "End Date cannot be earlier than Start Date.",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}
