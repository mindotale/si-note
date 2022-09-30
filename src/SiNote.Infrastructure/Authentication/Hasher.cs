using SiNote.Application.Common.Interfaces.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace SiNote.Infrastructure.Authentication;

public class Hasher : IHasher
{
    public (byte[] hash, byte[] salt) ComputeHash(string text)
    {
        byte[] hash, salt;
        using(var hmac = new HMACSHA256())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
        }
        return (hash, salt);
    }

    public byte[] ComputeHash(string text, byte[] salt)
    {
        byte[] hash;
        using (var hmac = new HMACSHA256(salt))
        {
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
        }
        return hash;
    }
}
