using Dominio;
using ExpectedObjects;

namespace Teste
{
	public class ProdutoTeste
	{
		private string descricao;
		private string codigoBarras;
		private decimal precoCompra;
		private decimal precoVenda;
		private int qtdEstoque;
		private string? categoria;
		private Guid? codigoFornecedor;

		private List<Produto> _produtoRepository = [];

		public ProdutoTeste()
		{
			descricao = "Feijão";
			codigoBarras = "123412341234";
			precoCompra = 0m;
			precoVenda = 8.90m;
			qtdEstoque = 0;
			categoria = "Alimentício";
			codigoFornecedor = Guid.NewGuid();

			Produto.LimparCache();
		}

		[Fact]
		public void CriarObjetoProduto()
		{
			var expectedProduto = new
			{
				Descricao = descricao,
				CodigoBarras = codigoBarras,
				PrecoCompra = precoCompra,
				PrecoVenda = precoVenda,
				QtdEstoque = qtdEstoque,
				Categoria = categoria,
				CodigoFornecedor = codigoFornecedor,
			};

			Produto produto = new Produto(
				expectedProduto.Descricao,
				expectedProduto.CodigoBarras,
				expectedProduto.PrecoCompra,
				expectedProduto.PrecoVenda,
				expectedProduto.QtdEstoque,
				expectedProduto.Categoria,
				expectedProduto.CodigoFornecedor


			   );

			expectedProduto.ToExpectedObject().ShouldMatch(produto);
		}

		[Fact]
		public void TestAtualizacaoEntidadePrecoVenda()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().Criar().AtualizaDado(0m, null, null)
			).ComMensagem("PrecoVenda é inválido. O valor quando atualizado deve ser diferente de zero.");
		}

		[Fact]
		public void TestAtualizacaoEntidadeCategoria()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().Criar().AtualizaDado(null, "    ", null)
			).ComMensagem("Categoria deve ser preenchido.");
		}

		[Fact]
		public void TestAtualizacaoEntidadeCodigoFornecedor()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().Criar().AtualizaDado(null, null, Guid.Empty)
			).ComMensagem("CodigoFornecedor é inválido.");
		}

		[Fact]
		public void TestDescricaoVazia()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComDescricao(string.Empty).Criar()
			).ComMensagem("Descricao é inválido.");
		}

		[Fact]
		public void TestCodigoBarrasEmpty()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComCodigoBarras(string.Empty).Criar()
			).ComMensagem("CodigoBarras é inválido.");
		}

		[Fact]
		public void TestPrecoCompraMaiorQueZero()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComPrecoCompra(10.99m).Criar()
			).ComMensagem("PrecoCompra é inválido. O valor ao registrar deve ser sempre zero.");
		}

		[Fact]
		public void TestPrecoVendaZero()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComPrecoVenda(0m).Criar()
			).ComMensagem("PrecoVenda é inválido.");
		}

		[Fact]
		public void TestQtdEstoqueZero()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComQtdEstoque(9999).Criar()
			).ComMensagem("QtdEstoque é inválido. O valor ao registrar deve ser sempre zero.");
		}

		[Fact]
		public void TestCategoriaVazia()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComCategoria(string.Empty).Criar()
			).ComMensagem("Categoria é inválido.");
		}

		[Fact]
		public void TestCodigoFornecedorEmpty()
		{
			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComCodigoFornecedor(Guid.Empty).Criar()
			).ComMensagem("CodigoFornecedor é inválido.");
		}

		[Fact]
		public void TestDuplicidadeCodBarras()
		{
			var produtoUm = ProdutoBuilder.Novo().Criar();

			Assert.Throws<ArgumentException>(
				() => ProdutoBuilder.Novo().ComCodigoBarras(produtoUm.CodigoBarras).Criar()
			).ComMensagem("Código de barras já foi cadastrado anteriormente para outro Produto.");
		}

		[Theory]
		[InlineData("Pesado")]
		[InlineData("Leve")]
		[InlineData("Podre")]
		public void GetByDescricao(string descricao)
		{
			var produto = ProdutoBuilder.Novo().ComDescricao(descricao).Criar();
			_produtoRepository = Produto.produtosCache;

			var repFakeBd = _produtoRepository.SingleOrDefault(c => c.Descricao.Contains(descricao));

			Assert.Equal(produto, repFakeBd);
		}

		[Theory]
		[InlineData("2741231230123")]
		[InlineData("53193123123")]
		[InlineData("001123123123")]
		public void GetByCodigoBarras(string codigoBarras)
		{
			var produto = ProdutoBuilder.Novo().ComCodigoBarras(codigoBarras).Criar();
			_produtoRepository = Produto.produtosCache;

			var repFakeBd = _produtoRepository.SingleOrDefault(c => c.CodigoBarras.Contains(codigoBarras));

			Assert.Equal(produto, repFakeBd);
		}

		[Theory]
		[InlineData("Alimenticio")]
		[InlineData("Higiene")]
		[InlineData("Textil")]
		public void GetByCategoria(string categoria)
		{
			var produto = ProdutoBuilder.Novo().ComCategoria(categoria).Criar();
			_produtoRepository = Produto.produtosCache;

			var repFakeBd = _produtoRepository.SingleOrDefault(c => c.Categoria.Contains(categoria));

			Assert.Equal(produto, repFakeBd);
		}

		[Fact]
		public void GetDetalhes()
		{
			var produtoOriginal = ProdutoBuilder.Novo().Criar();
			_produtoRepository = Produto.produtosCache;

			var detalhes = _produtoRepository.SingleOrDefault(c => c.Codigo == produtoOriginal.Codigo);

			Assert.NotNull(detalhes);
			Assert.Equal(produtoOriginal, detalhes);
		}
	}
}