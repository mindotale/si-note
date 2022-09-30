namespace SiNote.Application.Common.Interfaces.Authentication;

public interface IHasher
{
    public (byte[] hash, byte[] salt) ComputeHash(string password);
    public byte[] ComputeHash(string password, byte[] salt);
}
