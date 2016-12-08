using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FollowTheTask.BLL.Services.Security
{
    public class SecurityService : Service, ISecurityService
    {
        private readonly byte[] _pepper;

        public SecurityService()
        {
            _pepper = Guid.Parse(ConfigurationManager.AppSettings["pepper"]).ToByteArray();
        }


        public string GetNewSalt()
        {
            return ConcealSalt(Guid.NewGuid());
        }

        public string ApplySalt(string passwordText, string saltText)
        {
            var salt = RevealSalt(saltText).ToByteArray();
            var saltedPassword = Encoding.Unicode.GetBytes(passwordText).Concat(salt).ToArray();
            var hashAlgo = SHA256.Create();
            var hash = hashAlgo.ComputeHash(saltedPassword);
            var pepperedHash = hash.Concat(_pepper).ToArray();
            return Convert.ToBase64String(hashAlgo.ComputeHash(pepperedHash));
        }


        private string ConcealSalt(Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray());
        }

        private Guid RevealSalt(string saltText)
        {
            return new Guid(Convert.FromBase64String(saltText));
        }
    }
}