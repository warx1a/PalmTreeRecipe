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
            List<string> errors = new List<string>();
            if(oFactory.userValidation.ValidateAddUpdateProfileInfo(user, true, ref errors))
            {
                user = oFactory.userEndpoint.createUser(user);
                HttpContext.Session.SetString("sessionid", user.sessionId);
                Index idx = new Index();
                idx.featuredRecipes = new List<Recipe>();
                idx.latestRecipes = new List<Recipe>();
                return View("Index", idx);
            } else
            {
                user.errorMessages = errors;
                return View(user);
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
                    userProfile.errorMessages.Add("No user was able to be retrieved. Try logging in again");
                } else
                {
                    userProfile.user = loggedInUser;
                }
            //we weren't able to find a user for the session id. make them login again
            } else
            {
                userProfile.errorMessages.Add("No session info was stored. Try logging in again");
            }
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            Profile userProfile = new Profile();
            if(!string.IsNullOrEmpty(Request.HttpContext.Session.GetString("sessionid")))
            {
                User loggedInUser = oFactory.userEndpoint.getUserBySessionId(HttpContext.Session.GetString("sessionid"));
                if(loggedInUser != null)
                {
                    userProfile.user = loggedInUser;
                } else
                {
                    userProfile.errorMessages.Add("No user was able to be retrieved. Try logging in again");
                }
            } else
            {
                userProfile.errorMessages.Add("No session info was stored. Try logging in again");
            }
            return View(userProfile);
        }
        
        [HttpPost]
        public ActionResult UpdateProfile(Profile p)
        {
            List<string> errors = new List<string>();
            if(oFactory.userValidation.ValidateAddUpdateProfileInfo(p.user, false, ref errors))
            {
                if(oFactory.userEndpoint.updateUser(p.user))
                {
                    return View("Profile", p);
                } else
                {
                    errors.Add("Your profile was unable to be updated. Please try again");
                    p.user.errorMessages = errors;
                    return View(p);
                }
            } else
            {
                p.user.errorMessages = errors;
                return View(p);
            }
        }

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            Recipe recipe = new Recipe();
            return View(recipe);
        }

        [HttpPost]
        public ActionResult CreateRecipe(Recipe recipe)
        {
            User loggedInUser = oFactory.userEndpoint.getUserBySessionId(HttpContext.Session.GetString("sessionid"));
            if (loggedInUser == null)
            {
                return View("Login");
            }
            else
            {
                List<string> errors = new List<string>();
                if(oFactory.recipeValidation.ValidateAddUpdateRecipe(recipe, ref errors))
                {
                    recipe.UserID = loggedInUser.userId;
                    recipe.CreatedOnDateTime = DateTime.Now;
                    recipe = oFactory.recipeEndpoint.addRecipe(recipe);
                } else
                {
                    recipe.errorMessages = errors;
                    return View(recipe);
                }
            }
            //TODO: add in the save logic for recipes here
            return View(recipe);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
