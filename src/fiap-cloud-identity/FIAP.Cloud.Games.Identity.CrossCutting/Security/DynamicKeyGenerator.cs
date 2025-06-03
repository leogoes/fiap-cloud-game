using System.Security.Cryptography;

namespace FIAP.Cloud.Games.Identity.CrossCutting.Security
{
    public class DynamicKeyGenerator
    {
        public static byte[] GenerateKey(int bytes)
        {
            var data = new byte[bytes];
            RandomNumberGenerator.Create().GetBytes(new byte[bytes]);
            return data;
        }
    }
}
