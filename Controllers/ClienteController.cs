using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteAPI.Interfaces;
using TesteAPI.Models;
using TesteAPI.Repositories;

namespace TesteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]

    public class ClienteController : ControllerBase
    {

        private IClienteRepository clienteRepository { get; set; }

        public ClienteController(IConfiguration configuration)
        {
            clienteRepository = new ClienteRepository(configuration.GetConnectionString("Default"));

        }

        [HttpPost]
        [Authorize]
        public string Create(Cliente client)
        {
            try
            {
                int idUsuario = int.Parse(HttpContext.User.Claims.First(x => x.Type == "id").Value);
                client.UsuarioId = idUsuario;
                var result = clienteRepository.CreateCliente(client);
                return (result) ? "Cliente criado com sucesso" : "Erro na criação";

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
                var result = clienteRepository.GetClientePorUsuario(idUsuario);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("PorUsuario/{id}")]
        [Authorize]
        public IActionResult List(int id)
        {
            try
            {
                var result = clienteRepository.GetClientePorUsuario(id);
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}