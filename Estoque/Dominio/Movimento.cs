namespace Dominio
{
    public class Movimento
    {
        private Guid codigo;
        private int qtd;
        private decimal vlrCompra;
        private DateTime dtEntrada;
        private Guid codigoOrigem;
        private string numeroFatura;
        private string motivoEntrada;
        private string numIdentificacao;

        public Guid Codigo { get => codigo; set => codigo = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public decimal VlrCompra { get => vlrCompra; set => vlrCompra = value; }
        public DateTime DtEntrada { get => dtEntrada; set => dtEntrada = value; }
        public Guid CodigoOrigem { get => codigoOrigem; set => codigoOrigem = value; }
        public string NumeroFatura { get => numeroFatura; set => numeroFatura = value; }
        public string MotivoEntrada { get => motivoEntrada; set => motivoEntrada = value; }
        public string NumIdentificacao { get => numIdentificacao; set => numIdentificacao = value; }

        public static List<Movimento> movimentoCache = [];

        public Movimento(int qtd, decimal vlrCompra, DateTime dtEntrada, Guid codigoOrigem, string numeroFatura, string motivoEntrada, string numIdentificacao)
        {
            if (qtd == 0)
                throw new ArgumentException($"{nameof(Qtd)} deve ser informado.");

            if (vlrCompra == 0)
                throw new ArgumentException($"{nameof(VlrCompra)} deve ser informado.");

            if (dtEntrada == DateTime.MinValue)
                throw new ArgumentException($"{nameof(DtEntrada)} deve ter a composição de dados válida.");

            if (codigoOrigem == Guid.Empty)
                throw new ArgumentException($"{nameof(CodigoOrigem)} deve ter a composição de dados válida.");

            if (string.IsNullOrWhiteSpace(numeroFatura))
                throw new ArgumentException($"{nameof(NumeroFatura)} deve ter a composição de dados válida.");

            if (string.IsNullOrWhiteSpace(motivoEntrada))
                throw new ArgumentException($"{nameof(MotivoEntrada)} deve ter a composição de dados válida.");

            if (string.IsNullOrWhiteSpace(numIdentificacao))
                throw new ArgumentException($"{nameof(NumIdentificacao)} deve ter a composição de dados válida.");

            Codigo = Guid.NewGuid();
            Qtd = qtd;
            VlrCompra = vlrCompra;
            DtEntrada = dtEntrada;
            CodigoOrigem = codigoOrigem;
            NumeroFatura = numeroFatura;
            MotivoEntrada = motivoEntrada;
            NumIdentificacao = numIdentificacao;

            movimentoCache.Add(this);
        }
    }
}
