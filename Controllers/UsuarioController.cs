using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAPI.Interfaces;
using TesteAPI.Models;
using TesteAPI.Repositories;
using TesteAPI.Services;

namespace TesteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]

    public class UsuarioController : ControllerBase
    {

        private JwtServices jwtService { get; set; }

        private IUsuarioRepository usuarioRepository { get; set; }

        public UsuarioController(IConfiguration configuration)
        {
            var x = configuration.GetSection("JwtSettings").Get<JwtConfig>();
            jwtService = new JwtServices(x);
            usuarioRepository = new UsuarioRepository(configuration.GetConnectionString("Default"));

        }

        [HttpPost]
        public string Create(Usuario user)
        {
            try
            {
                var result = usuarioRepository.CreateUsuario(user);
                return (result) ? "Usuario criado com sucesso" : "Erro na criação";

            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult List()
        {
            try
            {
                int idUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value);
                var result = usuarioRepository.GetUsuario(idUsuario);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        public string Login(LoginDto user)
        {
            try
            {
                var result = usuarioRepository.LoginUsuario(user);
                if (result != null)
                    return jwtService.GerarToken(result);
                else throw new Exception("Usuario não encontrado");

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}