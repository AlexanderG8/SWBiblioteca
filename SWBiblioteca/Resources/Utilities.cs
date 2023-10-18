using System.Security.Cryptography;
using System.Text;

namespace SWBiblioteca.Resources
{
    public class Utilities
    {
        public static string EncryptKey(string clave)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] resul = hash.ComputeHash(enc.GetBytes(clave));
                foreach (byte b in resul)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }

        public string GenerarCodigoAleatorio()
        {
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder codigo = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                int indice = random.Next(0, caracteresPermitidos.Length);
                codigo.Append(caracteresPermitidos[indice]);
            }

            return codigo.ToString();
        }
    }
}
