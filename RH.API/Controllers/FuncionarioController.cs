using Microsoft.AspNetCore.Mvc;
using RH.API.DTOs;
using RH.API.Enum;
using RH.API.Models;
using RH.API.ViewsModels;

using RH.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RH.API.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

       
        [HttpPost("cadastrar-funcionario")]
        [Authorize(Roles = "Administrador")]
        public IActionResult CadastrarFuncionario([FromBody] Funcionario funcionario)
        {
            _funcionarioService.CadastrarFuncionario(funcionario);
            return Created("api/funcionario", funcionario);
        }

      
        [HttpDelete("{id}/excluir-funcionario")]
        [Authorize(Roles = "Administrador, Gerente")]
        public IActionResult ExcluirFuncionario([FromRoute] int id)
        {
            var funcionario = _funcionarioService.ObterPorId(id);
            if (funcionario.Id == id && funcionario.Role == Permissoes.Funcionario)
            {
                _funcionarioService.Excluir(id);
                return NoContent();
            }
            return BadRequest(new ErrorDTO("Id nao pertence a funcionario"));
        }

        [HttpDelete("{id}/excluir-gerente")]
        [Authorize(Roles = "Administrador")]
        public IActionResult ExcluirGerente([FromRoute] int id)
        {
            var funcionario = _funcionarioService.ObterPorId(id);
            if (funcionario.Id == id && funcionario.Role == Permissoes.Gerente)
            {
                _funcionarioService.Excluir(id);
                return NoContent();
            }
            return BadRequest(new ErrorDTO("Id nao pertence a gerente"));
        }

        [HttpPatch("{id}/alterar-salario")]
        [Authorize(Roles = "Gerente")]
        public IActionResult AlterarSalario([FromRoute] int id, [FromBody] SalarioDTO salario)
        {
            var funcionario = _funcionarioService.AlterarSalario(id, salario.Salario);

            return Ok(funcionario);
        }


        [HttpGet("listar")]
        [Authorize]
        public IActionResult ListarFuncionarios()
        {
            if (User.IsInRole(Permissoes.Funcionario.GetDisplayName()))
            {
                return Ok(_funcionarioService.ListagemParaFuncionarios());
            }
            return Ok(_funcionarioService.ListarFuncionarios());
        }

    }
}
