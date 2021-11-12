using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WepProject.Models;

namespace WepProject.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string senha, string cep, string cidade, string estado,
                string logradouro, string bairro, string complemento)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(senha) || String.IsNullOrEmpty(cep) ||
                    String.IsNullOrEmpty(cidade) || String.IsNullOrEmpty(estado) || 
                    String.IsNullOrEmpty(logradouro) || String.IsNullOrEmpty(bairro) || 
                    String.IsNullOrEmpty(complemento)
                    ) return View();

            if (VerifyCredential.HasEmail(email)) 
            {
                ViewData["error"] = "Email já cadastrado";
                return View();
            }

            cep = cep.Replace("-", "");
            Address address = new Address(cep, cidade, estado, logradouro, bairro, complemento);
            User user = new User(email, senha, address); 

            if (user.AddToDb() > 0)
            {
                return View("../DashBoard/Index", user);
            }

            ViewData["error"] = "Houve um problema ao salvar os dados tente novamente";
            return View();
        }

        public Address GetAllAddressFromCep(string cep)
        {
            if (String.IsNullOrEmpty(cep)) return new Address();
            cep = cep.Replace("-", "");
            if (isInvalidCep(cep))
            {
                ViewData["errorCep"] = "Cep Invalido";
                return new Address();
            }

            try
            {
                var request = (HttpWebRequest)WebRequest.Create($"https://viacep.com.br/ws/{cep}/json/");
                using (var checkService = (HttpWebResponse)request.GetResponse())
                {
                    if (checkService.StatusCode == HttpStatusCode.OK)
                    {
                        using (var webStream = checkService.GetResponseStream())
                        {
                            if (webStream != null)
                            {
                                using (var responseReader = new StreamReader(webStream))
                                {
                                    string response = responseReader.ReadToEnd();
                                    var options = new JsonSerializerOptions
                                    {
                                        PropertyNameCaseInsensitive = true,
                                    };
                                    return JsonSerializer.Deserialize<Address>(response, options);
                                }
                            }
                        }
                    }
                    ViewData["errorCep"] = "Funcionalidade Indisponivel no momento";
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Cep InvalidoWex");
                ViewData["errorCep"] = "Cep Invalido";
                Console.WriteLine("WebException Raised. The following error occurred : {0}", e.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cep InvalidoEx");
                ViewData["errorCep"] = "Cep Invalido";
                Console.WriteLine("\nThe following Exception was raised : {0}",e.Message);
            }
            return new Address();
        }

        private bool isInvalidCep(string cep)
        {
            var regex = new Regex("[A-Za-z -/:-@[-`{-~]");
            return (regex.IsMatch(cep) || cep.Length < 8);
        }
    }
}
