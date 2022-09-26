using RH.API.DTOs;
using RH.API.Enum;
using RH.API.Models;

namespace RH.API.Repositories
{
    public class FuncionarioRepository
    {
   

        private static List<Funcionario> funcionarios = new List<Funcionario>
        {
            new Funcionario
            {
                Id = 1,
                Nome = "Dayane",
                Email = "dayane@gmail.com",
                Senha = "123",
                Salario = 1500,
                Role = Permissoes.Administrador
            },  
            new Funcionario
            {
                Id = 1,
                Nome = "Sueli",
                Email = "sueli@gmail.com",
                Senha = "123",
                Salario = 1500,
                Role = Permissoes.Gerente
            },   
            new Funcionario
            {
                Id = 1,
                Nome = "Mikaelly",
                Email = "mikaelly@gmail.com",
                Senha = "123",
                Salario = 1500,
                Role = Permissoes.Funcionario
            },
        };

        public List<Funcionario> ObterTodosFuncionarios()
        {
            return funcionarios;
        }
        public Funcionario ObterPorEmailESenha(LoginDTO login)
        {
            var usuario = funcionarios.FirstOrDefault(x => x.Email == login.Email && x.Senha == login.Senha);

            return usuario;

        }
        public Funcionario AdicionaFuncionario(Funcionario funcionario)
        {
            funcionario.Id = GerarId();
           
            funcionarios.Add(funcionario);

            return funcionario ;
        }
        public int GerarId()
        {
            return funcionarios.Last().Id + 1;
        }
        public Funcionario ObterPorId(int id)
        {
            return funcionarios.Find(Funcionario => Funcionario.Id == id);
        }

        public Funcionario AtualizarFuncionario(int id)
        {
            var funcinarioAtual = ObterPorId(id);

            return funcinarioAtual;
        }

        public void RemoverFuncionario(int id)
        {
            var funcionario = ObterPorId(id);

            if (funcionario == null) return;

            funcionarios.Remove(funcionario);
        }


    }
}
