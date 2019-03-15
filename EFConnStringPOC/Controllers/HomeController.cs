using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFConnStringPOC.Models;
using EFConnStringPOC.Context;

namespace EFConnStringPOC.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainDbContext db;

        public HomeController(MainDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var customers = db.Customers.ToList();
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
