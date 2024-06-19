using Encryption.Api.Models;
using Encryption.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Encryption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionController(EncryptionService encryptionService) : ControllerBase
    {
        private readonly EncryptionService _encryptionService = encryptionService;

        [HttpGet]
        public async Task<IActionResult> GenerateEncryptionKey()
        {
            var genKey = await _encryptionService.GenerateEncryptionKey();

            return Ok(genKey);
        }

        [HttpPost("encryptData")]
        public async Task<IActionResult> Encryption([FromBody] Encrypt encrypt)
        {
            var enpt = await _encryptionService.EncryptData(encrypt);
            return Ok(enpt);
        }

        [HttpPost("decryptData")]
        public async Task<IActionResult> Decryption([FromBody] Decrypt decrypt)
        {
            var dept = await _encryptionService.DecryptData(decrypt);
            return Ok(dept);
        }
    }
}
