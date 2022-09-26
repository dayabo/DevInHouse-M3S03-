using Microsoft.OpenApi.Extensions;
using RH.API.Models;

namespace RH.API.ViewsModels
{
    public class FuncionarioListaViewModel
    {
        public FuncionarioListaViewModel()
        { }
        public FuncionarioListaViewModel(Funcionario funcionario)
           {
                Nome = funcionario.Nome;
               
                Permissao = funcionario.Role.GetDisplayName();
            }

        public string Nome { get; set; }
        public string Permissao { get; set; }
    }
}
