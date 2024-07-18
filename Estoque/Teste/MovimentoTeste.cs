using Dominio;
using ExpectedObjects;

namespace Teste
{
	public class MovimentoTeste
	{
		private int qtd;
		private decimal vlrCompra;
		private DateTime dtEntrada;
		private Guid codigoOrigem;
		private string numeroFatura;
		private string motivoEntrada;
		private string numIdentificacao;

		protected List<Movimento> _movimentoRepository = [];

		public MovimentoTeste()
		{
			motivoEntrada = "Transferência";
			codigoOrigem = Guid.NewGuid();
			qtd = 300;
			numeroFatura = "150123";
			vlrCompra = 13.123m;
			numIdentificacao = "98765454";
			dtEntrada = DateTime.Now;
		}

		[Fact]
		public void CriarObjetoMovimento()
		{
			var expectedMovimento = new
			{
				Qtd = qtd,
				VlrCompra = vlrCompra,
				DtEntrada = dtEntrada,
				CodigoOrigem = codigoOrigem,
				NumeroFatura = numeroFatura,
				MotivoEntrada = motivoEntrada,
				NumIdentificacao = numIdentificacao,
			};

			Movimento movimento = new Movimento(
				expectedMovimento.Qtd,
				expectedMovimento.VlrCompra,
				expectedMovimento.DtEntrada,
				expectedMovimento.CodigoOrigem,
				expectedMovimento.NumeroFatura,
				expectedMovimento.MotivoEntrada,
				expectedMovimento.NumIdentificacao
			);

			expectedMovimento.ToExpectedObject().ShouldMatch(movimento);
		}

		[Fact]
		public void CriarObjetoMovimento2()
		{
			var expectedMovimento = new
			{
				Qtd = 300,
				VlrCompra = 13.123m,
				DtEntrada = DateTime.Now,
				NumeroFatura = "150123",
				CodigoOrigem = Guid.NewGuid(),
				MotivoEntrada = "Transferência",
				NumIdentificacao = "98765454"
			};

			Movimento movimento = new Movimento(
				expectedMovimento.Qtd,
				expectedMovimento.VlrCompra,
				expectedMovimento.DtEntrada,
				expectedMovimento.CodigoOrigem,
				expectedMovimento.NumeroFatura,
				expectedMovimento.MotivoEntrada,
				expectedMovimento.NumIdentificacao
			);

			expectedMovimento.ToExpectedObject().ShouldMatch(movimento);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void TestMotivoEntradaEmpty(string motivoEntrada)
		{
			Assert.Throws<ArgumentException>(
				() => MovimentoBuilder.Novo().ComMotivoEntrada(motivoEntrada).Criar()
			).ComMensagem("MotivoEntrada deve ter a composição de dados válida.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("   ")]
		public void TestNumIdentificacaoEmpty(string numIdentificacao)
		{
			Assert.Throws<ArgumentException>(
				() => MovimentoBuilder.Novo().ComNumIdentificacao(numIdentificacao).Criar()
			).ComMensagem("NumIdentificacao deve ter a composição de dados válida.");
		}

		[Fact]
		public void GetAll()
		{
			MovimentoBuilder.Novo().Criar();

			Assert.NotNull(Movimento.movimentoCache);
		}
	}
}

