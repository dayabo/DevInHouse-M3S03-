using Microsoft.OpenApi.Extensions;
using RH.API.DTOs;
using RH.API.Enum;
using RH.API.Models;
using RH.API.Repositories;
using RH.API.ViewsModels;

namespace RH.API.Services
{
    public class FuncionarioService
    {
        private readonly FuncionarioRepository _funcionarioRepository;

        public FuncionarioService(FuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public Funcionario Logar(LoginDTO login)
        {
            return (_funcionarioRepository.ObterPorEmailESenha(login));


        }
        public void CadastrarFuncionario(Funcionario funcionario)
        {
            _funcionarioRepository.AdicionaFuncionario(funcionario);
        }

        public void Excluir(int id)
        {
            _funcionarioRepository.RemoverFuncionario(id);
        }

        public Funcionario ObterPorId(int id)
        {
            return _funcionarioRepository.ObterPorId(id);
         
        }

        public Funcionario AlterarSalario(int id, decimal salario)
        {
            var funcionario = _funcionarioRepository.AtualizarFuncionario(id);

            funcionario.Salario = salario;

             return funcionario;
        }

        public List<FuncionarioViewModel> ListarFuncionarios()
        {
            return (List<FuncionarioViewModel>)_funcionarioRepository.ObterTodosFuncionarios()
                .Select(f => new FuncionarioViewModel(f)).ToList();
        }
        public List<FuncionarioListaViewModel> ListagemParaFuncionarios()
        {
            return (List<FuncionarioListaViewModel>)_funcionarioRepository.ObterTodosFuncionarios()
                .Select(f => new FuncionarioListaViewModel(f)).ToList();
        }
        public List<Funcionario> ListarTodosFuncionarios()
        {
            return _funcionarioRepository.ObterTodosFuncionarios();

        }
    }
}
