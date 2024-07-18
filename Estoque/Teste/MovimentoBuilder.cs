using Dominio;

namespace Teste
{
    public class MovimentoBuilder
    {
        private string _motivoEntrada = "Transferência";
        private Guid _codigoOrigem = Guid.NewGuid();
        private int _qtd = 300;
        private string _numeroFatura = "150123";
        private decimal _vlrCompra = 13.123m;
        private string _numIdentificacao = "98765454";
        private DateTime _dtEntrada = DateTime.Now;

        public static MovimentoBuilder Novo()
        {
            return new MovimentoBuilder();
        }

        public Movimento Criar()
        {
            return new Movimento(_qtd, _vlrCompra, _dtEntrada,
                _codigoOrigem, _numeroFatura, _motivoEntrada, _numIdentificacao);
        }

        public MovimentoBuilder ComQtd(int qtd)
        {
            _qtd = qtd;
            return this;
        }

        public MovimentoBuilder ComVlrCompra(decimal vlrCompra)
        {
            _vlrCompra = vlrCompra;
            return this;
        }

        public MovimentoBuilder ComDtEntrada(DateTime dtEntrada)
        {
            _dtEntrada = dtEntrada;
            return this;
        }

        public MovimentoBuilder ComCodigoOrigem(Guid codigoOrigem)
        {
            _codigoOrigem = codigoOrigem;
            return this;
        }

        public MovimentoBuilder ComNumeroFatura(string numeroFatura)
        {
            _numeroFatura = numeroFatura;
            return this;
        }

        public MovimentoBuilder ComMotivoEntrada(string motivoEntrada)
        {
            _motivoEntrada = motivoEntrada;
            return this;
        }

        public MovimentoBuilder ComNumIdentificacao(string numIdentificacao)
        {
            _numIdentificacao = numIdentificacao;
            return this;
        }
    }
}
