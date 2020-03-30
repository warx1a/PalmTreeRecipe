using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalmTreeRecipe.Connectors;
using PalmTreeRecipe.Models;

namespace PalmTreeRecipe.Controllers
{
    public class HomeController : Controller
    {

        private Factory oFactory = new Factory();
        private string alphaNumericRegex = @"(^[a-zA-Z0-9]+$)";

        [HttpGet]
        public ActionResult Index()
        {
            Index idx = new Index();
            //TODO: populate these fields w/ latest and featured recipes
            idx.featuredRecipes = new List<Recipe>();
            idx.latestRecipes = new List<Recipe>();
            return View(idx);
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
            //add user validation here for the postback
            //username validation
            if(string.IsNullOrEmpty(user.username))
            {
                user.errorMessages.Add("Username cannot be empty");
            } else if(!Regex.IsMatch(user.username, alphaNumericRegex))
            {
                user.errorMessages.Add("Username must be alphanumeric");
            } else if(user.username.Length < 8)
            {
                user.errorMessages.Add("Username must be at least 5 characters");
            }
            //password validation
            if(string.IsNullOrEmpty(user.password))
            {
                user.errorMessages.Add("Password cannot be empty");
            } else if(string.IsNullOrEmpty(user.confirmPassword))
            {
                user.errorMessages.Add("The confirmation password cannot be empty");
            } else if(!user.password.Equals(user.confirmPassword))
            {
                user.errorMessages.Add("The password and the confirmation must match");
            } else if(user.password.Length < 8)
            {
                user.errorMessages.Add("The password must be at least 8 characters long");
            }
            //if we have errors display them back to the user
            if(user.errorMessages.Count > 0)
            {
                return View(user);
            } else
            {
                user = oFactory.userEndpoint.createUser(user);
                HttpContext.Session.SetString("sessionid", user.sessionId);
                Index idx = new Index();
                idx.featuredRecipes = new List<Recipe>();
                idx.latestRecipes = new List<Recipe>();
                return View("Index", idx);
            }
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
            //add validation here to check the username/pwd combo
            if(string.IsNullOrEmpty(login.username))
            {
                login.errorMessages.Add("Username cannot be empty");
            }
            if(string.IsNullOrEmpty(login.password))
            {
                login.errorMessages.Add("Password cannot be empty");
            }
            string sessionId = oFactory.userEndpoint.login(login.username, login.password);
            //failed to login
            if(sessionId == null)
            {
                login.errorMessages.Add("Invalid username or password");
            } else
            //we were successful so set the session id
            {
                Response.HttpContext.Session.SetString("sessionid", sessionId);
                Index idx = new Index();
                idx.featuredRecipes = new List<Recipe>();
                idx.latestRecipes = new List<Recipe>();
                return View("Index", idx);
            }
            return View(login);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            Profile userProfile = new Profile();
            if(!string.IsNullOrEmpty(Request.HttpContext.Session.GetString("sessionid")))
            {
                User loggedInUser = oFactory.userEndpoint.getUserBySessionId(HttpContext.Session.GetString("sessionid"));
                if(loggedInUser == null)
                {
                    userProfile.errorMessages.Add("No user was able to be retrieved. Try loggin in again");
                } else
                {
                    userProfile.user = loggedInUser;
                }
            //we weren't able to find a user for the session id. make them login again
            } else
            {
                userProfile.errorMessages.Add("No user was able to be retrieved. Try logging in again");
            }
            return View(userProfile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
