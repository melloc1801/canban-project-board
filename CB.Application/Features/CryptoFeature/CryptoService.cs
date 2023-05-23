using System.Security.Cryptography;
using System.Text;

namespace CB.Application.Features.CryptoFeature;

public class CryptoService: ICryptoService
{
    public string ComputeSHA256Hash(string str)
    {
        if (str == null)
        {
            throw new ArgumentNullException();
        }
        
        using var sha256 = SHA256.Create();
        var hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
        return BitConverter.ToString(hashValue).Replace("-", "");
    }
}