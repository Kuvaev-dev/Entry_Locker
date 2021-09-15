using Entry_Locker.Models;
using Entry_Locker.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Entry_Locker.Controllers
{
    public class HomeController : Controller
    {
        private User_Repository user_Repository = new User_Repository();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Task.Run(() =>
            {
                long ip = Request.HttpContext.Connection.RemoteIpAddress.Address;
                string date = DateTime.Now.Day.ToString() + ":" + DateTime.Now.Month.ToString() + ":" + DateTime.Now.Year.ToString();
                string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                user_Repository.Autorezation(new User()
                {
                    IP = ip,
                    Date = date,
                    Time = time
                });
            });
            return View();
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
