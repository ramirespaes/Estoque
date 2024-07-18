using Dominio.Shared;

namespace Dominio
{
    public class Produto : EntityBase
    {        //campos
		protected Guid codigo;
		protected string descricao;
		protected string codigoBarras;
		protected decimal precoCompra;
		protected decimal precoVenda;
		protected int qtdEstoque;
		protected string? categoria;
		protected Guid? codigoFornecedor;

        //propriedades

        public Guid Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public string CodigoBarras { get => codigoBarras; set => codigoBarras = value; }
        public decimal PrecoCompra { get => precoCompra; set => precoCompra = value; }
        public decimal PrecoVenda { get => precoVenda; set => precoVenda = value; }
        public int QtdEstoque { get => qtdEstoque; set => qtdEstoque = value; }
        public string? Categoria { get => categoria; set => categoria = value; }
        public Guid? CodigoFornecedor { get => codigoFornecedor; set => codigoFornecedor = value; }

        public static List<Produto> produtosCache = [];

        public Produto(string descricao, string codigoBarras, decimal precoCompra, decimal precoVenda, int qtdEstoque, string? categoria, Guid? codigoFornecedor)
        {
            if (produtosCache.Any(p => p.CodigoBarras == codigoBarras)) 
                throw new ArgumentException("Código de barras já foi cadastrado anteriormente para outro Produto.");

            if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException($"{nameof(Descricao)} é inválido.");
            if (string.IsNullOrWhiteSpace(codigoBarras)) throw new ArgumentException($"{nameof(CodigoBarras)} é inválido.");
            if (precoCompra != 0) throw new ArgumentException($"{nameof(PrecoCompra)} é inválido. O valor ao registrar deve ser sempre zero.");
            if (precoVenda <= 0) throw new ArgumentException($"{nameof(PrecoVenda)} é inválido.");
            if (qtdEstoque != 0) throw new ArgumentException($"{nameof(QtdEstoque)} é inválido. O valor ao registrar deve ser sempre zero.");
            if (categoria != null && string.IsNullOrWhiteSpace(categoria)) throw new ArgumentException($"{nameof(Categoria)} é inválido.");
            if (codigoFornecedor != null && codigoFornecedor == Guid.Empty) throw new ArgumentException($"{nameof(CodigoFornecedor)} é inválido.");

            Codigo = Guid.NewGuid();
            Descricao = descricao;
            CodigoBarras = codigoBarras;
            PrecoCompra = precoCompra;
            PrecoVenda = precoVenda;
            QtdEstoque = qtdEstoque;
            Categoria = categoria;
            CodigoFornecedor = codigoFornecedor;

            produtosCache.Add(this);
        }

        public void AtualizaDado(decimal? precoVenda, string? categoria, Guid? codigoFornecedor)
        {
            if (precoVenda != null)
            {
                if (precoVenda <= 0)
                    throw new ArgumentException($"{nameof(PrecoVenda)} é inválido. O valor quando atualizado deve ser diferente de zero.");

				PrecoVenda = precoVenda.GetValueOrDefault(0);
			}

            if (categoria != null)
            {
                if (string.IsNullOrWhiteSpace(categoria))
                    throw new ArgumentException($"{nameof(Categoria)} deve ser preenchido.");

				Categoria = categoria;
			}

            if (codigoFornecedor != null)
            {
                if (codigoFornecedor == Guid.Empty)
                    throw new ArgumentException($"{nameof(CodigoFornecedor)} é inválido.");

				CodigoFornecedor = codigoFornecedor;
			}
        }

		public static void LimparCache()
		{
            produtosCache.Clear();
		}
	}
}
