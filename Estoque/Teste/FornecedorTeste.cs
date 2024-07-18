using Dominio;
using ExpectedObjects;

namespace Teste
{
	public class FornecedorTeste
	{
		private string nomeEmpresa;
		private string endereco;
		private string telefone;
		private string email;
		private string termosPagamento;

		protected List<Fornecedor> _fornecedorRepository = [];

		public FornecedorTeste()
		{
			nomeEmpresa = "Azure";
			endereco = "Rua Americana";
			telefone = "55123123";
			email = "azure@support.com";
			termosPagamento = "Only Paypal";

			Fornecedor.LimparCache();
		}

		[Fact]
		public void CriarObjetoFornecedor3()
		{
			var expectedFornecedor = new
			{
				NomeEmpresa = nomeEmpresa,
				Endereco = endereco,
				Telefone = telefone,
				Email = email,
				TermosPagamento = termosPagamento
			};

			Fornecedor @for = new Fornecedor(
				expectedFornecedor.NomeEmpresa,
				expectedFornecedor.Endereco,
				expectedFornecedor.Telefone,
				expectedFornecedor.Email,
				expectedFornecedor.TermosPagamento
			   );

			expectedFornecedor.ToExpectedObject().ShouldMatch(@for);
		}

		[Fact]
		public void CriarObjetoCliente2()
		{
			var expectedFornecedor = new
			{
				NomeEmpresa = "Azure",
				Endereco = "Rua Americana",
				Telefone = "55123123",
				Email = "azure@support.com",
				TermosPagamento = "Only Paypal",
			};

			Fornecedor @for = new Fornecedor(
				expectedFornecedor.NomeEmpresa,
				expectedFornecedor.Endereco,
				expectedFornecedor.Telefone,
				expectedFornecedor.Email,
				expectedFornecedor.TermosPagamento
			);

			expectedFornecedor.ToExpectedObject().ShouldMatch(@for);
		}
		[Fact]
		public void TestAtualizacaoEntidadeEndereco()
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().Criar().AtualizadarDados(string.Empty, "091234810392")
			).ComMensagem("Endereco deve ser informado.");
		}

		[Fact]
		public void TestAtualizacaoEntidadeTelefone()
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().Criar().AtualizadarDados("Street Americana", string.Empty)
			).ComMensagem("Telefone deve ser informado.");
		}

		[Fact]
		public void FornecedorDuplicidade()
		{
			var fornecedorOriginal = FornecedorBuilder.Novo().Criar();

			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().ComNome(fornecedorOriginal.NomeEmpresa).ComTelefone(fornecedorOriginal.Telefone).Criar()
			).ComMensagem("Combinação de Nome e Telefone já se encontra cadastrada.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void FornecedorNomeEmpty(string nome)
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().ComNome(nome).Criar()
			).ComMensagem("NomeEmpresa deve ser informado.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void FornecedorEnderecoEmpty(string endereco)
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().ComEndereco(endereco).Criar()
			).ComMensagem("Endereco deve ser informado.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void FornecedorTelefoneEmpty(string telefone)
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().ComTelefone(telefone).Criar()
			).ComMensagem("Telefone deve ser informado.");
		}

		[Theory]
		[InlineData("aaaaaaaaaaaaaaaa")]
		[InlineData("11111111111")]
		[InlineData("*")]
		[InlineData("@.")]
		public void FornecedorEmail(string email)
		{
			Assert.Throws<ArgumentException>(
				() => FornecedorBuilder.Novo().ComEmail(email).Criar()
			).ComMensagem("Email deve ter a composição de dados válida.");
		}

		[Theory]
		[InlineData("Azure")]
		[InlineData("AWS")]
		[InlineData("Totvs")]
		public void GetByNome(string nome)
		{
			var fornecedor = FornecedorBuilder.Novo().ComNome(nome).Criar();
			_fornecedorRepository = Fornecedor.fornecedoresCache;

			var repFakeBd = _fornecedorRepository.SingleOrDefault(c => c.NomeEmpresa.Contains(nome));

			Assert.Equal(fornecedor, repFakeBd);
		}

		[Theory]
		[InlineData("Azure@hotmail.com")]
		[InlineData("AWS@gmail.com")]
		[InlineData("Totvs@hotmail.com")]
		public void GetByEmail(string email)
		{
			var fornecedor = FornecedorBuilder.Novo().ComEmail(email).Criar();
			_fornecedorRepository = Fornecedor.fornecedoresCache;

			var repFakeBd = _fornecedorRepository.SingleOrDefault(c => c.Email.Contains(email));

			Assert.Equal(fornecedor, repFakeBd);
		}

		[Theory]
		[InlineData("1234")]
		[InlineData("4321")]
		[InlineData("0987")]
		public void GetByTelefone(string telefone)
		{
			var fornecedor = FornecedorBuilder.Novo().ComTelefone(telefone).Criar();
			_fornecedorRepository = Fornecedor.fornecedoresCache;

			var repFakeBd = _fornecedorRepository.SingleOrDefault(c => c.Telefone.Contains(telefone));

			Assert.Equal(fornecedor, repFakeBd);
		}

		[Fact]
		public void GetDetalhes()
		{
			var fornecedorOriginal = FornecedorBuilder.Novo().Criar();
			_fornecedorRepository = Fornecedor.fornecedoresCache;

			var detalhes = _fornecedorRepository.SingleOrDefault(c => c.Codigo == fornecedorOriginal.Codigo);

			Assert.NotNull(detalhes);
			Assert.Equal(fornecedorOriginal, detalhes);
		}
	}
}

