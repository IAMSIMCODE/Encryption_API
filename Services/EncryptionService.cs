using Encryption.Api.Models;

namespace Encryption.Api.Services
{
    public interface IEncryptionService
    {
        Task<GenerateKeyRes> GenerateEncryptionKey();
        Task<EncryptResponse> EncryptData(Encrypt encrypt);
        Task<DecryptResponse> DecryptData(Decrypt decrypt);
    }

    public class EncryptionService : IEncryptionService
    {
        public async Task<DecryptResponse> DecryptData(Decrypt decrypt)
        {
            var plainText = Utility.Decrypt(decrypt.CipherText, decrypt.AesKey, decrypt.AesIvKey);

            var deRes = new DecryptResponse() { PlainText = plainText.ToString().TrimEnd(new char[] {'\0'}) };
            return await Task.FromResult(deRes);
        }

        public Task<EncryptResponse> EncryptData(Encrypt encrypt)
        {
            var ciphText = Utility.Encrypt(encrypt.PlainText, encrypt.AesKey, out var iVKey);

            var enRes = new EncryptResponse() { AesIvKey = iVKey, CipherText = ciphText };
            return Task.FromResult(enRes);
        }

        public async Task<GenerateKeyRes> GenerateEncryptionKey()
        {
            var encKey = Utility.GenerateKey();

            var enk = new GenerateKeyRes() { AesKey = encKey };
            return await Task.FromResult(enk);
        }
    }
}
