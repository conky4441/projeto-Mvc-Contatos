using System.Security.Cryptography;
using System.Text;

namespace ProjetoMVC.Api.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string senha)
        {
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(senha);
            array = SHA1.HashData(array);

            var sb = new StringBuilder();

            foreach (var item in array)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}

