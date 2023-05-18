namespace TesteAPI.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }
        public string Telefone { get; set; }

        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCriacao { get; set; }


    }
}
