using System.Text;

namespace UStart.Domain.Helpers.TokenHelper
{
    public class TokenContext
    {
        public string SecretToken { get; private set; }


        /// <summary>
        /// Caso zero então usa o padrão de 60 minutos
        /// </summary>
        public int ExpiresInMinutes { get; private set; }


        public TokenContext(string secretToken, int timeExpiresInMinutes)
        {
            this.SecretToken = secretToken;
            ExpiresInMinutes = timeExpiresInMinutes != 0
                ? timeExpiresInMinutes
                : 60;
        }

        public byte[] getTokenBytes()
        {
            return Encoding.ASCII.GetBytes(SecretToken);
        }
    }
}