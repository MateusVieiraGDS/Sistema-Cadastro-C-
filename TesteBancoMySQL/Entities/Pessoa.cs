using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoMySQL
{
    class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Nascimento { get; set; }

        public Pessoa(string nome, string email, string telefone, DateTime nascimento, int id = -1)
        {
            ID = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Nascimento = nascimento;
        }
        public override string ToString()
        {
            return $"[#{ID}]Nome: {Nome}\nNascimento: {Nascimento.ToString("dd/MM/yyyy")}\nE-mail: {Email}\nTelefone: {Nascimento}";
        }
    }
}

