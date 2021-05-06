using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tampieri.Model;

namespace TestTampieri
{
    [TestClass]
    public class LivingCharactersTest
    {
        private ILivingCharacter PLAYER = new Player("Jane Appleseed");
        [TestMethod]
        public void TestMovements()
        {
            Assert.IsTrue(new Player("Jane Appleseed").CanPerform(Action.Move));
            Assert.IsFalse(new Player("Jane Appleseed").CanPerform(Action.Idle));
        }

        [TestMethod]
        public void TestPotion()
        {
            IHealthModifier badPotion = new SimplePotion("Bad potion", -20);
            IHealthModifier goodPotion = new SimplePotion("GoodPotion", 15);
            badPotion.UseOn(PLAYER);
            goodPotion.UseOn(PLAYER);
            Assert.AreEqual(PLAYER.Health, 95);
        }

        [TestMethod]
        public void TestHealthOverflow()
        {
            IHealthModifier goodPotion = new SimplePotion("GoodPotion", 20);
            for (int i = 0; i < 6; i++)
            {
                goodPotion.UseOn(PLAYER);
            }
            Assert.AreEqual(PLAYER.Health, 100);
        }

        [TestMethod]
        public void TestHealthUnderflow()
        {
            IHealthModifier badPotion = new SimplePotion("Bad potion", -20);
            for (int i = 0; i < 6; i++)
            {
                badPotion.UseOn(PLAYER);
            }
            Assert.AreEqual(PLAYER.Health, 0);
            Assert.IsTrue(PLAYER.IsAlive());
        }
    }
}
