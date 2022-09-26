using RH.API.DTOs;
using RH.API.Enum;

namespace RH.API.Models
{
    public class Funcionario
    {
        public int Id{ get; internal set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha{ get; set; }
        public decimal Salario { get; set; }
        public Permissoes Role { get; set; }

       
    }
}
