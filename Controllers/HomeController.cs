using System.Diagnostics;
using System.Threading.Tasks;
using TripsLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace TripsLog.Controllers
{
    public class HomeController : Controller
    {


        private TripsDbContext context { get; set; }

        public HomeController(TripsDbContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var trips = context.Trips
                .Include(t => t.Accommodation)
                .Include(t => t.Todos)
                .OrderBy(c => c.Destination)
                .ToList();
            return View(trips);
        }


    }
}
