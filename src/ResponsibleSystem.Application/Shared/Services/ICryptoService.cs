namespace ResponsibleSystem.Shared.Services
{
    public interface ICryptoService
    {
        string Encrypt(string plaintext);

        string Decrypt(string encryptedText);
    }
}