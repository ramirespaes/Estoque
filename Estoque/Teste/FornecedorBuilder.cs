using Dominio;

namespace Teste
{
    public class FornecedorBuilder
    {
        private string _nomeEmpresa = "Zé Colmeia";
        private string _endereco = "parque yellowstrone";
        private string _telefone = "3422-2121";
        private string _email = "zecolmeia@gmail.com";
        private string _termosPagamento = "Somente PIX";

        public static FornecedorBuilder Novo()
        {
            return new FornecedorBuilder();
        }

        public Fornecedor Criar()
        {
            return new Fornecedor(_nomeEmpresa, _endereco, _telefone,
                _email, _termosPagamento);
        }

        public FornecedorBuilder ComNome(string nome)
        {
            _nomeEmpresa = nome;
            return this;
        }

        public FornecedorBuilder ComEndereco(string endereco)
        {
            _endereco = endereco;
            return this;
        }

        public FornecedorBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public FornecedorBuilder ComTelefone(string telefone)
        {
            _telefone = telefone;
            return this;
        }

        public FornecedorBuilder ComTermosPagamento(string termosPagamento)
        {
            _termosPagamento = termosPagamento;
            return this;
        }
    }
}
