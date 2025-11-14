using System.ComponentModel.DataAnnotations;

namespace TripsLog.Models
{
    public class Accommodation
    {
        public int AccommodationId { get; set; }

        [Display(Name = "Accommodation")]
        public string? Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }

    }
}
