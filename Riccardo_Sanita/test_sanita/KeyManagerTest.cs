using Microsoft.VisualStudio.TestTools.UnitTesting;
using sanita.input;
using System.Text;

namespace Test_sanita
{
    [TestClass]
    public class KeyManagerTest
    {
        [TestMethod]
        public void ManagerTest()
        {
            KeyManager.Manager.KeyPressed('w');
            int AsciCode = (int)'w';
            Assert.IsTrue(KeyManager.Manager.IsKeyPressed(AsciCode));
            KeyManager.Manager.KeyReleased('w');
            Assert.IsFalse(KeyManager.Manager.IsKeyPressed(AsciCode));
        }
    }
}
