using Microsoft.AspNetCore.Mvc;
using RH.API.DTOs;
using RH.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public AutenticacaoController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        // POST api/<AutenticacaoController>
        [HttpPost]
        [Route ("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
           var user =  _funcionarioService.Logar(login);

            if (user == null) return Unauthorized();
            var token = TokenService.GeraToken(user);

            return Ok(token);
        }

       
    }
}
