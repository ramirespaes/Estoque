using System.Net.Mail;

namespace Dominio.Extensions
{
    public static class StringExtensions
    {
        public static bool ValidarEmail(this string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
