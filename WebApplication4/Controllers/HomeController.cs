using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Models.Entity;
using WebApplication4.Models.Request;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private HomeServices homeServices;

        public HomeController(HomeServices sessions)
        {
            homeServices = sessions;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (homeServices.Login(username, password))
            {
                HttpContext.Session.SetString("username", username);
                return View("Privacy");
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task SendMessage([FromQueryAttribute]string message)
        {
            await homeServices.SendMessageToAll(message);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
