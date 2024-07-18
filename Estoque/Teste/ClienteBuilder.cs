using Dominio;

namespace Teste
{
    public class ClienteBuilder
    {
        private string _nome = "Zé Colmeia";
        private string _endereco = "parque yellowstrone";
        private string _telefone = "3422-2121";
        private string _email = "zecolmeia@gmail.com";

        public static ClienteBuilder Novo()
        {
            return new ClienteBuilder();
        }

        public Cliente Criar()
        {
            return new Cliente(_nome, _endereco, _telefone,
                _email);
        }

        public ClienteBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ClienteBuilder ComEndereco(string endereco)
        {
            _endereco = endereco;
            return this;
        }

        public ClienteBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public ClienteBuilder ComTelefone(string telefone)
        {
            _telefone = telefone;
            return this;
        }
    }
}
