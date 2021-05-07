using Microsoft.VisualStudio.TestTools.UnitTesting;
using sanita;

namespace Test_sanita
{
    [TestClass]
    public class AssetManagerTest
    {
        [TestMethod]
        public void AManagerTest()
        {

           AssetManager am = AssetManager.AManager;
           Assert.IsNull(am.GetImage("ciaone"));
           Assert.IsNotNull(am.GetImage("Player"));
        }
    }
}
