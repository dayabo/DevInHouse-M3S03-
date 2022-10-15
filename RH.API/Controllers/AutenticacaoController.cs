using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            var user = _funcionarioService.Logar(login);

            var listRefresh = TokenService.GetAllRefreshToken();
            var contentUserName = listRefresh.Any(n =>  n.Item1 == user.Nome);
            

            if (user == null) return Unauthorized();

            if (contentUserName)
            {
                var newToken = TokenService.GeraToken(user);
                var newRefreshToken = TokenService.GenerateRefreshToken();
                TokenService.DeleteRefreshToken(user.Nome, TokenService.GetRefreshToken(user.Nome));
                TokenService.SaveRefreshToken(user.Nome, newRefreshToken);

                return Ok(new
                {
                    newToken,
                    newRefreshToken
                });
            }

            var token = TokenService.GeraToken(user);
            var refreshToken = TokenService.GenerateRefreshToken();

            TokenService.SaveRefreshToken(user.Nome, refreshToken);

            return Ok(new { token, refreshToken });
        }


        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public IActionResult Refresh(
            [FromQuery] string token,
            [FromQuery] string refreshToken
        )
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(username);

            if (savedRefreshToken != refreshToken)
                throw new SecurityTokenException("Invalid Token");

            var newToken = TokenService.GeraToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            TokenService.DeleteRefreshToken(username, refreshToken);
            TokenService.SaveRefreshToken(username, newRefreshToken);

            return Ok(new
            {
                newToken,
                newRefreshToken
            });
        }

    
    }
}