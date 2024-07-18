using Dominio.Extensions;
using Dominio.Shared;

namespace Dominio
{
    public class Cliente : EntityBase
    {
        //campos
        protected Guid codigo;
		protected string nome;
		protected string endereco;
		protected string telefone;
		protected string email;
		protected List<string> historicoCompras;

        //propriedades

        public Guid Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Telefone { get => telefone; set => telefone = value; }

        public string? Email { get => email; set => email = value; }
        public List<string>? HistoricoCompras { get => historicoCompras; set => historicoCompras = value; }

        public static List<Cliente> clientesCache = [];

        public Cliente(string nome, string endereco, string telefone, string? email)
        {
            if (clientesCache.Any(c => c.Nome == nome && c.Telefone == telefone))
                throw new ArgumentException($"Combinação de {nameof(Nome)} e {nameof(Telefone)} já cadastrada.");

            if (string.IsNullOrWhiteSpace(nome)) 
                throw new ArgumentException($"{nameof(Nome)} deve ser informado.");

            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException($"{nameof(Endereco)} deve ser informado.");

            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException($"{nameof(Telefone)} deve ser informado.");

            if (!string.IsNullOrWhiteSpace(email) && !email.ValidarEmail())
                throw new ArgumentException($"{nameof(Email)} deve ter a composição de dados válida.");

            this.Codigo = Guid.NewGuid();
            this.Nome = nome;
            this.Endereco = endereco;
            this.Telefone = telefone;
            this.Email = email;

            clientesCache.Add( this );
        }

        public void AtualizaDados(string endereco, string telefone)
        {
            if (string.IsNullOrWhiteSpace(endereco))
            {
                throw new ArgumentException($"{nameof(Endereco)} deve ser informado.");
            }
            else
				Endereco = endereco;

			if (string.IsNullOrWhiteSpace(telefone))
            {
                throw new ArgumentException($"{nameof(Telefone)} deve ser informado.");
            }
            else
                Telefone = telefone;
        }

		public static void LimparCache()
		{
            clientesCache.Clear();
		}
	}
}
