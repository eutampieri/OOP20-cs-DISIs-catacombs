using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tampieri.Model;

namespace TestTampieri
{
    [TestClass]
    public class ParsingTest
    {
        [TestMethod]
        public void TestValidActions()
        {
            Assert.AreEqual(Action.Attack, "attack".ToAction());
            Assert.AreEqual(Action.Move, "move".ToAction());
            Assert.AreEqual(Action.Die, "die".ToAction());
            Assert.AreEqual(Action.Idle, "idle".ToAction());
        }

        [TestMethod]
        public void TestInvalidActions()
        {
            Assert.IsNull("Attack".ToAction());
            Assert.IsNull("42".ToAction());
            Assert.IsNull("lorem ipsum".ToAction());
            Assert.IsNull("IDLE".ToAction());
        }

        [TestMethod]
        public void TestValidDirectionsForActions()
        {
            Assert.AreEqual(0, Action.Die.GetPossibleDirections().Count);
            Assert.IsTrue(Action.Move.GetPossibleDirections().Contains(Direction.Right));
        }
    }
}
