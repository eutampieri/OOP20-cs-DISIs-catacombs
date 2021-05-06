using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tampieri.Model;

namespace TestTampieri
{
    [TestClass]
    public class LivingCharactersTest
    {
        private Player PLAYER = new Player("Jane Appleseed");
        [TestMethod]
        public void TestMovements()
        {
            Assert.IsTrue(PLAYER.CanPerform(Action.Move));
            Assert.IsFalse(PLAYER.CanPerform(Action.Idle));
        }
    }
}
