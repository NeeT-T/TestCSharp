using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WepProject.Models;

namespace WepProject.Controllers
{

    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
            {
                User user = VerifyCredential.IsValidCredentials(email, password);
                if (user != null) 
                {
                    return View("../DashBoard/Index", user);
                }
                ViewData["error"] = "*Invalid credentials, verify your email or password";
            }
            return View();
        }
    }
}
