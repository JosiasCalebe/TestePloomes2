namespace TesteAPI.Models
{
    public class Usuario
    {
    public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
