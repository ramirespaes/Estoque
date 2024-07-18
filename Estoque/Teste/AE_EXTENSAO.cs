namespace Teste
{

    public static class AE_extensao

    {
        public static void ComMensagem(this ArgumentException excecao, string mensagem)

        {
            if (excecao.Message == mensagem)
            {
                Assert.True(true);
            }
            else
            {
                Assert.False(true, $"Esperava a mensagem {mensagem}");
            }

        }
    }
}


