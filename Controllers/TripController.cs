using TripsLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TripsLog.Controllers
{
    public class TripController : Controller
    {
        private TripsDbContext context { get; set; }

        public TripController(TripsDbContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Trip()
        {
            ViewBag.Categories = context.Trips.OrderBy(c => c.Destination).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult ProcessTrip(Trip trip)
        {
            // Store all trip data in TempData
            TempData["Destination"] = trip.Destination;
            TempData["StartDate"] = trip.StartDate.ToString("yyyy-MM-dd");
            TempData["EndDate"] = trip.EndDate.ToString("yyyy-MM-dd");

            if (!string.IsNullOrWhiteSpace(trip.Accommodation?.Name))
            {
                TempData["AccommodationName"] = trip.Accommodation.Name;

                //Redirect to Accommodation action if accommodation name is provided
                return RedirectToAction("Accommodation");
            }
            else 
            {
                // If no accommodation, skip to Todo view
                return RedirectToAction("Todo");
            }

            
        }

        [HttpGet]
        public IActionResult Accommodation()
        {
            var trip = new Trip();

            // Set ViewBag for back button logic
            ViewBag.HasDestination = TempData["Destination"] != null;
            TempData.Keep("Destination"); // Keep it for the back button check

            if (ViewBag.HasDestination)
            {
                if (TempData["AccommodationName"] != null)
                {
                    ViewBag.Title = $"Add info for {TempData["AccommodationName"]}";
                    TempData.Keep("AccommodationName");
                }
                else
                {
                    ViewBag.Title = "Add accommodation info";
                }
            }
            else
            {
                ViewBag.Title = "Add accommodation info";
            }


            return View(trip);
        }

        [HttpPost]
        public IActionResult ProcessAccommodation(Trip trip)
        {
            // Store accommodation details in TempData
            if (!string.IsNullOrEmpty(trip.Accommodation?.Phone))
            {
                TempData["AccommodationPhone"] = trip.Accommodation.Phone;
            }

            if (!string.IsNullOrEmpty(trip.Accommodation?.Email))
            {
                TempData["AccommodationEmail"] = trip.Accommodation.Email;
            }

            // Keep existing TempData values
            TempData.Keep("Destination");
            TempData.Keep("StartDate");
            TempData.Keep("EndDate");
            TempData.Keep("AccommodationName");

            return RedirectToAction("Todo");
        }

        [HttpGet]
        public IActionResult Todo()
        {
            var trip = new Trip
            {
                Todos = new List<Todo> { new Todo() } 
            };

            if (TempData["Destination"] != null)
            {
                ViewBag.Title = $"Things To Do in {TempData["Destination"]}";
                TempData.Keep("Destination");
            }
            else
            {
                ViewBag.Title = "Things To Do";
            }

            // Set ViewBag for back button logic
            ViewBag.HasAccommodation = TempData["AccommodationName"] != null;
            TempData.Keep("AccommodationName"); // Keep it for the back button check

            return View(trip);
        }

        [HttpPost]
        public IActionResult SaveTrip(string[] TodoName, bool[] TodoIsComplete)
        {
            // Read TempData values
            var destination = TempData["Destination"]?.ToString();
            var startDateStr = TempData["StartDate"]?.ToString();
            var endDateStr = TempData["EndDate"]?.ToString();
            var accommodationName = TempData["AccommodationName"]?.ToString();
            var accommodationPhone = TempData["AccommodationPhone"]?.ToString();
            var accommodationEmail = TempData["AccommodationEmail"]?.ToString();

            // Create and save Trip
            var completeTrip = new Trip
            {
                Destination = destination,
                StartDate = DateTime.Parse(startDateStr),
                EndDate = DateTime.Parse(endDateStr),
                Accommodation = !string.IsNullOrWhiteSpace(accommodationName) ? new Accommodation
                {
                    Name = accommodationName,
                    Phone = accommodationPhone,
                    Email = accommodationEmail
                } : null
            };

            context.Trips.Add(completeTrip);
            context.SaveChanges();

            // Save all todos
            if (TodoName != null)
            {
                for (int i = 0; i < TodoName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(TodoName[i]))
                    {
                        var isComplete = TodoIsComplete != null && i < TodoIsComplete.Length && TodoIsComplete[i];

                        var newTodo = new Todo
                        {
                            Name = TodoName[i],
                            IsComplete = isComplete,
                            TripId = completeTrip.TripId
                        };
                        context.Todos.Add(newTodo);
                    }
                }
                context.SaveChanges();
            }

            TempData.Clear();
            TempData["SuccessMessage"] = "Trip saved successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}