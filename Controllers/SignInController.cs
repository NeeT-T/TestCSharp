using System;
using System.Text.Json;
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
                var user = VerifyCredential.IsValidCredentials(email, password);
                if (user != null) 
                {
                    TempData["CurrentUser"] = JsonSerializer.Serialize<User>(user);
                    return RedirectToAction("Index", "DashBoard");
                }
                ViewData["error"] = "*Invalid credentials, verify your email or password";
            }
            return View();
        }
    }
}
