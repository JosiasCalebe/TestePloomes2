using System.Data;
using System.Data.SqlClient;
using TesteAPI.Interfaces;
using TesteAPI.Models;

namespace TesteAPI.Repositories
{
    public class UsuarioRepository : BaseRepository, IDisposable, IUsuarioRepository
    {
        private string _connectionString;
        public UsuarioRepository(string connString) : base(connString)
        {
            _connectionString = connString;
        }

        ~UsuarioRepository() 
        { 
            this.Dispose();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string Teste()
        {
            return _connectionString;
        }

        public bool CreateUsuario(Usuario user)
        {
            try
            {
                var result = Execute("INSERT INTO Usuarios (Nome, Email, Senha) VALUES (@_Nome, @_Email, @_Senha)", new {_Nome = user.Nome, _Email = user.Email, _Senha = user.Senha });
                return result != 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public Usuario GetUsuario(int id)
        {
            try
            {
                var result = QueryFirstOrDefault<Usuario>("Select UsuarioId, Nome, Email, Senha from Usuarios where UsuarioId = @_Id", new { _Id = id });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

        }

        public Usuario LoginUsuario(LoginDto login)
        {
            try
            {
                var result = QueryFirstOrDefault<Usuario>("Select UsuarioId, Nome, Email, Senha from Usuarios where Email = @_Email and Senha = @_Senha", new { _Email = login.Email, _Senha = login.Senha });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

        }
    }
}
