using System.Collections;
using NUnit.Framework;

namespace SQLCryptExt.Tests
{
    [TestFixture]
    class SQLCryptExtTest
    {
        [Test]
        public void text_is_hashed_with_specified_salt()
        {
            PBKDF2Hash.HashedBytes expected = new PBKDF2Hash.HashedBytes("Csf0jEKmYUVfsfk7UHVIKFmsyj8jrTiVWPNxEp4NEdDdKcOTd2YCUX+RPJncsSu5unRbCDgeAtWFTbtsIHZe0w==", "5000.thisisasalt");
            ArrayList actualResult = (ArrayList)PBKDF2Hash.PBKDF2HashList("12345", "5000.thisisasalt");
            PBKDF2Hash.HashedBytes actual = (PBKDF2Hash.HashedBytes)actualResult[0];
            Assert.AreEqual(expected.Hash, actual.Hash);
            Assert.AreEqual(expected.Salt, actual.Salt);
        }

        [Test]
        public void text_is_hashed_with_new_salt_if_salt_is_not_specified()
        {
            PBKDF2Hash.HashedBytes expected = new PBKDF2Hash.HashedBytes("Csf0jEKmYUVfsfk7UHVIKFmsyj8jrTiVWPNxEp4NEdDdKcOTd2YCUX+RPJncsSu5unRbCDgeAtWFTbtsIHZe0w==", "5000.thisisasalt");
            ArrayList actualResult = (ArrayList)PBKDF2Hash.PBKDF2HashList("12345", "5000.thisisasalt");
            actualResult = (ArrayList)PBKDF2Hash.PBKDF2HashList("12345", null);
            PBKDF2Hash.HashedBytes actual = (PBKDF2Hash.HashedBytes)actualResult[0];
            Assert.AreNotEqual(expected.Hash, actual.Hash);
            Assert.AreNotEqual(expected.Salt, actual.Salt);
        }
    }
}
