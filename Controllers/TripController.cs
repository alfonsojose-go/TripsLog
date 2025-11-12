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
            // Store accommodation name in TempData
            if (!string.IsNullOrEmpty(trip.Accommodation?.Name))
            {
                TempData["AccommodationName"] = trip.Accommodation.Name;
            }

            //Store destination name in TempData
            if (TempData["Destination"] != null)
            {
                trip.Destination = TempData["Destination"].ToString();
                // Store destination for the Todo view
                TempData["Destination"] = TempData["Destination"]; // Keep it
            }

            // Store other trip data if needed
            TempData["Destination"] = trip.Destination;
            TempData["StartDate"] = trip.StartDate;
            TempData["EndDate"] = trip.EndDate;

            return RedirectToAction("Accommodation");
        }

        [HttpGet]
        public IActionResult Accommodation()
        {
            // Create a new Trip object or get from repository
            var trip = new Trip();

            // Set ViewBag title using TempData
            if (TempData["AccommodationName"] != null)
            {
                ViewBag.Title = $"Add info for {TempData["AccommodationName"]}";
                // Keep the TempData for the next request
                TempData.Keep("AccommodationName");
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
            // Retrieve the accommodation name from TempData
            if (TempData["AccommodationName"] != null)
            {
                trip.Accommodation.Name = TempData["AccommodationName"].ToString();
            }

            // Retrieve other trip data
            if (TempData["Destination"] != null)
            {
                trip.Destination = TempData["Destination"].ToString();
            }

            // Save the complete trip data to database or session
            // _tripRepository.Save(trip);

            return RedirectToAction("Todo");
        }



        [HttpGet]
        public IActionResult Todo()
        {
            // Create a new Trip object
            var trip = new Trip();

            // Set ViewBag title using Destination from TempData
            if (TempData["Destination"] != null)
            {
                ViewBag.Title = $"Things To Do in {TempData["Destination"]}";
                TempData.Keep("Destination"); // Keep it for future requests
            }
            else
            {
                ViewBag.Title = "Things To Do";
            }

            return View(trip);
        }

        [HttpPost]
        public IActionResult SaveTrip(Trip trip)
        {
            // Retrieve all stored data from TempData
            if (TempData["Destination"] != null)
            {
                trip.Destination = TempData["Destination"].ToString();
            }

            if (TempData["StartDate"] != null)
            {
                trip.StartDate = DateTime.Parse(TempData["StartDate"].ToString());
            }

            if (TempData["EndDate"] != null)
            {
                trip.EndDate = DateTime.Parse(TempData["EndDate"].ToString());
            }

            if (TempData["AccommodationName"] != null)
            {
                trip.Accommodation.Name = TempData["AccommodationName"].ToString();
            }

            // Now you have the complete trip object with all data
            // Save to database
            // _tripRepository.Save(trip);

            return RedirectToAction("Index", "Home"); // Or wherever you want to go after saving
        }
    }
}
