using System;
using System.Web;
using Abp.Runtime.Security;
using ResponsibleSystem.Shared.Services;

namespace ResponsibleSystem
{
    public class TokenHelper
    {
        private readonly ICryptoService _cryptoService;

        public TokenHelper(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public string SignToken(string token)
        {
            var t = _cryptoService.Encrypt(token);
            return HttpUtility.UrlEncode(t);
        }

        public string ExtractTokenIfValid(string token)
        {
            try
            {
                string decryptedToken = null;
                try
                {
                    decryptedToken = _cryptoService.Decrypt(token);
                }
                catch
                {
                    var urlDecoded = HttpUtility.UrlDecode(token);
                    decryptedToken = _cryptoService.Decrypt(urlDecoded);
                }
                var resetToken = new SimpleStringCipher().Decrypt(decryptedToken);
                var data = resetToken.Split('|');
                var utcTicks = long.Parse(data[2]);

                if (DateTime.UtcNow - new DateTime(utcTicks) > TimeSpan.FromHours(1))
                {
                    return null;
                }

                return decryptedToken;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}