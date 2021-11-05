using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WepProject.Models;

namespace WepProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (Request.Method == "POST")
            {
                string email = Request.Form["email"];
                string password = Request.Form["password"];
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
                {
                    UserCredential userCredential = new UserCredential(email, password);
                    LoginValidation loginValidation = new LoginValidation();
                
                    if (loginValidation.isValidCredentials(userCredential)) 
                    {
                        return View("../Home/DashBoard");
                    }
                    ViewData["error"] = "*Invalid credentials, verify your email or password";
                }
            }
            
            return View();
        }

    }
}
