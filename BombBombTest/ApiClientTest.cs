using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BombBombTest
{
    [TestClass]
    public class ApiClientTest
    {
        [TestMethod]
        public void TestGoodLogin()
        {
            BombBomb.ApiClient c = new BombBomb.ApiClient("yourname@bombbomb.com", "yourpassword");
            Assert.IsTrue(c.isLoginValid(), "That login should have worked!");
        }

        [TestMethod]
        public void TestBadLogin()
        {
            BombBomb.ApiClient c = new BombBomb.ApiClient("bademailaddress", "badpassword");
            Assert.IsFalse(c.isLoginValid(), "That login should not have worked!");
        }
    }
}
