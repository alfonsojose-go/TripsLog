using System.ComponentModel.DataAnnotations;

namespace TripsLog.Models
{
    public class Trip
    {
        public int TripId { get; set; }
        public string Destination { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int AccomodationId { get; set; }

        public Accommodation Accommodation { get; set; } = new Accommodation();

        public int TodoId { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();


    }
}
