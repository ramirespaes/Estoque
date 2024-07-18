using Dominio.Extensions;
using Dominio.Shared;

namespace Dominio
{
	public class Fornecedor : EntityBase
    {
        //campos
        protected Guid codigo;
		protected string nomeEmpresa;
		protected string endereco;
		protected string telefone;
		protected string? email;
		protected string? termosPagamento;

        //propriedades

        public Guid Codigo { get => codigo; set => codigo = value; }
        public string NomeEmpresa { get => nomeEmpresa; set => nomeEmpresa = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Telefone { get => telefone; set => telefone = value; }

        public string? Email { get => email; set => email = value; }
        public string? TermosPagamento { get => termosPagamento; set => termosPagamento = value; }

        public static List<Fornecedor> fornecedoresCache = [];

        public Fornecedor(string nome, string endereco, string telefone, string? email, string termosPagamento)
        {
            if (fornecedoresCache.Any(f => f.NomeEmpresa == nome && f.Telefone == telefone))
                throw new ArgumentException("Combinação de Nome e Telefone já se encontra cadastrada.");

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException($"{nameof(NomeEmpresa)} deve ser informado.");

            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException($"{nameof(Endereco)} deve ser informado.");

            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException($"{nameof(Telefone)} deve ser informado.");

            if (!string.IsNullOrWhiteSpace(email) && !email.ValidarEmail())
                throw new ArgumentException($"{nameof(Email)} deve ter a composição de dados válida.");

            if (termosPagamento != null && string.IsNullOrWhiteSpace(termosPagamento))
                throw new ArgumentException($"{nameof(TermosPagamento)} deve ter a composição de dados válida.");

            this.Codigo = Guid.NewGuid();
            this.NomeEmpresa = nome;
            this.Endereco = endereco;
            this.Telefone = telefone;
            this.Email = email;
            this.termosPagamento = termosPagamento;

            fornecedoresCache.Add( this );
        }

        public void AtualizadarDados(string endereco, string telefone)
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
            fornecedoresCache.Clear();
		}
	}
}
