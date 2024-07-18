using Dominio;
using ExpectedObjects;

namespace Teste
{
	public class ClienteTeste
	{
		private string nome;
		private string endereco;
		private string telefone;
		private string? email => null;

		protected List<Cliente> _clienteRepository = [];

		public ClienteTeste()
		{
			this.nome = "João Domingues";
			this.endereco = "Rua Eugenio Moreira";
			this.telefone = "(47) 99133-0912";

			Cliente.LimparCache();
		}

		[Fact]
		public void CriarObjetoCliente3()
		{
			var expectedCliente = new
			{
				Nome = nome,
				Endereco = endereco,
				Telefone = telefone,
				Email = email
			};

			Cliente cli = new Cliente(
				expectedCliente.Nome,
				expectedCliente.Endereco,
				expectedCliente.Telefone,
				expectedCliente.Email
			   );

			expectedCliente.ToExpectedObject().ShouldMatch(cli);
		}

		[Fact]
		public void CriarObjetoCliente2()
		{
			var expectedCliente = new
			{
				Nome = "João Domingues",
				Endereco = "Rua Eugenio Moreira",
				Telefone = "(47) 99133-0912",
				Email = email
			};

			Cliente cli = new Cliente(
				expectedCliente.Nome,
				expectedCliente.Endereco,
				expectedCliente.Telefone,
				expectedCliente.Email
			);

			expectedCliente.ToExpectedObject().ShouldMatch(cli);
		}

		[Fact]
		public void TestAtualizacaoEntidadeEndereco()
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().Criar().AtualizaDados(string.Empty, "34558122")
			).ComMensagem("Endereco deve ser informado.");
		}

		[Fact]
		public void TestAtualizacaoEntidadeTelefone()
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().Criar().AtualizaDados("Rua Porto Uniao", string.Empty)
			).ComMensagem("Telefone deve ser informado.");
		}

		[Fact]
		public void TestDuplicidadeNome()
		{
			var clienteOriginal = ClienteBuilder.Novo().Criar();

			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().ComNome(clienteOriginal.Nome).ComTelefone(clienteOriginal.Telefone).Criar()
			).ComMensagem("Combinação de Nome e Telefone já cadastrada.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void ClienteNomeEmpty(string nome)
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().ComNome(nome).Criar()
			).ComMensagem("Nome deve ser informado.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void ClienteEnderecoEmpty(string endereco)
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().ComEndereco(endereco).Criar()
			).ComMensagem("Endereco deve ser informado.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void ClienteTelefoneEmpty(string telefone)
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().ComTelefone(telefone).Criar()
			).ComMensagem("Telefone deve ser informado.");

		}

		[Theory]
		[InlineData("zzzzzzzzzzz")]
		[InlineData("931231931293129")]
		[InlineData("******")]
		[InlineData("@a.com.br.org.edu")]
		public void ClienteEmail(string email)
		{
			Assert.Throws<ArgumentException>(
				() => ClienteBuilder.Novo().ComEmail(email).Criar()
			).ComMensagem("Email deve ter a composição de dados válida.");
		}

		[Theory]
		[InlineData("Luis")]
		[InlineData("Joao")]
		[InlineData("Carlos")]
		public void GetByNome(string nome)
		{
			var cliente = ClienteBuilder.Novo().ComNome(nome).Criar();
			_clienteRepository = Cliente.clientesCache;

			var repFakeBd = _clienteRepository.SingleOrDefault(c => c.Nome.Contains(nome));

			Assert.Equal(cliente, repFakeBd);
		}

		[Theory]
		[InlineData("luis@hotmail.com")]
		[InlineData("Joao@gmail.com")]
		[InlineData("Carlos@hotmail.com")]
		public void GetByEmail(string email)
		{
			var cliente = ClienteBuilder.Novo().ComEmail(email).Criar();
			_clienteRepository = Cliente.clientesCache;

			var repFakeBd = _clienteRepository.SingleOrDefault(c => c.Email.Contains(email));

			Assert.Equal(cliente, repFakeBd);
		}

		[Theory]
		[InlineData("1234")]
		[InlineData("4321")]
		[InlineData("0987")]
		public void GetByTelefone(string telefone)
		{
			var cliente = ClienteBuilder.Novo().ComTelefone(telefone).Criar();
			_clienteRepository = Cliente.clientesCache;

			var repFakeBd = _clienteRepository.SingleOrDefault(c => c.Telefone.Contains(telefone));

			Assert.Equal(cliente, repFakeBd);
		}


		[Fact]
		public void GetDetalhes()
		{
			Cliente clienteOriginal = ClienteBuilder.Novo().Criar();
			_clienteRepository = Cliente.clientesCache;

			var detalhes = _clienteRepository.SingleOrDefault(c => c.Codigo == clienteOriginal.Codigo);

			Assert.NotNull(detalhes);
			Assert.Equal(clienteOriginal, detalhes);
		}

	}
}