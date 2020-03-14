using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PalmTreeRecipe.Models;

namespace PalmTreeRecipe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            User u = new User();
            return View(u);
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            return View(user);
        }

        [HttpGet]
        public ActionResult Login()
        {
            Login l = new Login();
            return View(l);
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            return View(login);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
