using TesteAPI.Models;

namespace TesteAPI.Interfaces
{
    public interface IClienteRepository
    {
        bool CreateCliente(Cliente user);
        Cliente GetCliente(int id);
        List<Cliente> GetClientePorUsuario(int id);


    }
}
