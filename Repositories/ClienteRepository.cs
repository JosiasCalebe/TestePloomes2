using System.Data;
using System.Data.SqlClient;
using TesteAPI.Interfaces;
using TesteAPI.Models;

namespace TesteAPI.Repositories
{
    public class ClienteRepository : BaseRepository, IDisposable, IClienteRepository
    {
        private string _connectionString;
        public ClienteRepository(string connString) : base(connString)
        {
            _connectionString = connString;
        }

        ~ClienteRepository() 
        { 
            this.Dispose();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool CreateCliente(Cliente client)
        {
            try
            {
                var result = Execute("INSERT INTO Clientes (UsuarioId, Nome, Sobrenome, Email, Telefone, Endereco, DataNascimento, DataCriacao) " +
                    "VALUES (@_UsuarioId, @_Nome, @_Sobrenome,@_Email, @_Telefone, @_Endereco, @_DataNascimento, GETDATE())", 
                    
                    new { _UsuarioId = client.UsuarioId, 
                        _Nome = client.Nome, 
                        _Sobrenome = client.Sobrenome, 
                        _Email = client.Email, 
                        _Telefone = client.Telefone, 
                        _Endereco = client.Endereco, 
                        _DataNascimento  = client.DataNascimento});
                return result != 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public Cliente GetCliente(int id)
        {
            try
            {
                var result = QueryFirstOrDefault<Cliente>("Select * from Clientes where ClienteId = @_Id", new { _Id = id });
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

        }

        public List<Cliente> GetClientePorUsuario(int id)
        {
            try
            {
                var result = Query<Cliente>("Select * from Clientes where UsuarioId = @_Id", new { _Id = id });
                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }

        }
    }
}
