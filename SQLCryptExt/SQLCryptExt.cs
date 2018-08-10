using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Security;
using SimpleCrypto;

[assembly: AllowPartiallyTrustedCallers]
namespace SQLCryptExt
{
    public class PBKDF2Hash
    {
        public class HashedBytes
        {
            public SqlString Hash;
            public SqlString Salt;
            public HashedBytes(SqlString text, SqlString salt)
            {
                Hash = text;
                Salt = salt;
            }
        }

        [Microsoft.SqlServer.Server.SqlFunction(
            FillRowMethodName = "Hash",
            TableDefinition = "Hash NVARCHAR(MAX), Salt NVARCHAR(MAX)"
            )]
        public static IEnumerable PBKDF2HashList(string text, string salt)
        {
            ICryptoService cryptoService = new PBKDF2();
            ArrayList hashedText = new ArrayList();
            string hash;

            if (salt == null)
            {
                hash = cryptoService.Compute(text, 16, 5000);
                salt = cryptoService.Salt;
            }
            else
            {
                hash = cryptoService.Compute(text, salt);
            }

            hashedText.Add(new HashedBytes(hash, salt));

            return hashedText;
        }

        public static void Hash(Object objText, out SqlString hash, out SqlString salt)
        {
            HashedBytes hashedBytes = (HashedBytes)objText;
            hash = hashedBytes.Hash;
            salt = hashedBytes.Salt;
        }
    }
}
