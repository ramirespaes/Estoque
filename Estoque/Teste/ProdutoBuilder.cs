using Dominio;

namespace Teste
{
    public class ProdutoBuilder
    {
        private string _descricao = "Feijão";
        private string _codigoBarras = "123412341234";
        private decimal _precoCompra = 0m;
        private decimal _precoVenda = 8.90m;
        private int _qtdEstoque = 0;
        private string? _categoria = "Alimentício";
        private Guid? _codigoFornecedor = Guid.NewGuid();

        public static ProdutoBuilder Novo()
        {
            return new ProdutoBuilder();
        }

        public Produto Criar()
        {
            return new Produto(_descricao, _codigoBarras, _precoCompra,
                _precoVenda, _qtdEstoque, _categoria, _codigoFornecedor);
        }

        public ProdutoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ProdutoBuilder ComCodigoBarras(string codigoBarras)
        {
            _codigoBarras = codigoBarras;
            return this;
        }

        public ProdutoBuilder ComPrecoCompra(decimal precoCompra)
        {
            _precoCompra = precoCompra;
            return this;
        }

        public ProdutoBuilder ComPrecoVenda(decimal precoVenda)
        {
            _precoVenda = precoVenda;
            return this;
        }

        public ProdutoBuilder ComQtdEstoque(int qtdEstoque)
        {
            _qtdEstoque = qtdEstoque;
            return this;
        }

        public ProdutoBuilder ComCategoria(string categoria)
        {
            _categoria = categoria;
            return this;
        }

        public ProdutoBuilder ComCodigoFornecedor(Guid codigoFornecedor)
        {
            _codigoFornecedor = codigoFornecedor;
            return this;
        }
    }
}
