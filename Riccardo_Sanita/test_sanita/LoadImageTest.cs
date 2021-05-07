using Microsoft.VisualStudio.TestTools.UnitTesting;
using sanita.utils;
using System.Drawing;
using System.IO;

namespace Test_sanita
{
    [TestClass]
    public class LoadImageTest
    {
        [TestMethod]
        public void TestLoadImage()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath1 = Path.Combine(projectPath, "Resources/playersheet.png");
            string filePath2 = Path.Combine(projectPath, "Resources/eagle.png");

            Image img = ImageLoader.LoadImage(filePath2);
            Assert.IsNull(img);
            Image img1 = ImageLoader.LoadImage(filePath1);
            Assert.IsNotNull(img1);
        }
    }
}
