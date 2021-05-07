using Microsoft.VisualStudio.TestTools.UnitTesting;
using sanita.gamefx;
using System.IO;

namespace Test_sanita
{
    [TestClass]
    public class GameSheetTest
    {
        [TestMethod]
        public void TestGameSheets()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources/playersheet.png");

            GameSheet gs = new GameSheet(filePath);
            Assert.IsNotNull(gs.Picture);
        }
    }
}
