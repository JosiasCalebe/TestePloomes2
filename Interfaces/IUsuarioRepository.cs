using TesteAPI.Models;

namespace TesteAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        bool CreateUsuario(Usuario user);
        Usuario GetUsuario(int id);
        Usuario LoginUsuario(LoginDto login);

    }
}
