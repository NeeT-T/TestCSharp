using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WepProject.Models;

namespace WepProject.Controllers
{
    public class DashBoard : Controller
    {
        public IActionResult Index()
        {
            TempData.Keep("CurrentUser");
            ViewBag.CurrentUser = JsonSerializer.Deserialize<User>(TempData["CurrentUser"].ToString());
            return View(new User().GetUsers());
        }

        public IActionResult User(int id)
        {
            return View("UserInfo", new User().GetUserById(id));
        }

        [HttpPost]
        public IActionResult User(int id, string email, string senha, string cep, string cidade, string estado,
                string logradouro, string bairro, string complemento)
        {
            cep = cep.Replace("-", "");
            Address address = new Address(cep, cidade, estado, logradouro, bairro, complemento);
            User newUserData = new User(id, email, senha, address); 

            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(senha) || String.IsNullOrEmpty(cep) ||
                    String.IsNullOrEmpty(cidade) || String.IsNullOrEmpty(estado) || 
                    String.IsNullOrEmpty(logradouro) || String.IsNullOrEmpty(bairro) || 
                    String.IsNullOrEmpty(complemento)
                    ) {
                return View("UserInfo", newUserData);
            }

            User currentUserData = new User().GetUserById(newUserData.Id);
            if (newUserData.Equals(currentUserData))
            {
                ViewData["success"] = "Os dados inseridos são identicos aos dados já cadastrados";
                return View("UserInfo", currentUserData);
            }
            if (newUserData.Email != currentUserData.Email)
            {
                if (newUserData.HasEqualEmail())
                {
                    ViewData["error"] = "Email já foi registrado por outro usuário";
                    return View("UserInfo", newUserData);
                }
            }

            if (newUserData.UpdateUser() > 0)
            {
                ViewData["success"] = "Dados atualizados";
                return View("UserInfo", newUserData);
            }
            return View("UserInfo", currentUserData);
        }

        public IActionResult RemoveUser(int id)
        {
            TempData["retorno"] = (new User().RemoveUser(id) > 0) ? true : false;
            return RedirectToActionPermanent("Index", "DashBoard");
        }
    }
}
