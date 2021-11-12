using System;

namespace WepProject.Models
{
    public class Address
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; } 
            
        public Address() { }

        public Address(string cep, string cidade, string estado, string logradouro, string bairro, string complemento)
        {
            Cep = cep;
            Localidade = cidade;
            UF = estado;
            Logradouro = logradouro;
            Bairro = bairro;
            Complemento = complemento;
        }
    }
}
