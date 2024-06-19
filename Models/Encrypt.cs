namespace Encryption.Api.Models
{
    public class Encrypt
    {
        public string AesKey { get; set; }
        public string PlainText { get; set; }
    }

    public class Decrypt
    {
        public string AesKey { get; set; }
        public string AesIvKey { get; set; }
        public string CipherText { get; set; }
    }

    public class GenerateKeyRes
    {
        public string AesKey { get; set; }
    }

    public class EncryptResponse
    {
        public string AesIvKey { get; set; }
        public string CipherText { get; set; }
    }

    public class DecryptResponse
    {
        public string PlainText { get; set; }
    }
}
