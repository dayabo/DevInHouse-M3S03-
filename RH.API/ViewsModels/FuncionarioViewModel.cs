using Microsoft.OpenApi.Extensions;
using RH.API.Models;

namespace RH.API.ViewsModels
{
    public class FuncionarioViewModel
    {
        public FuncionarioViewModel() { }
        public FuncionarioViewModel(Funcionario funcionario)
        {
            Id = funcionario.Id;
            Nome = funcionario.Nome;
            Email = funcionario.Email;
            Senha = funcionario.Senha;
            Salario = funcionario.Salario;
            Permissao = funcionario.Role.GetDisplayName();
        }

        public int Id { get; internal set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public decimal Salario { get; set; }
       public string Permissao { get; set; }

    }
}
