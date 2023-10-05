using MoonActive.Scripts.Class;
using MoonActive.Scripts.Managers;
using NUnit.Framework;

namespace MoonActive.Scripts.Test.EditMode
{
    public class ConfigTest
    {
        [Test]
        public void ConfigTestSimplePasses()
        {
            var gameConfigs = DataManager.LoadDataXML<GameConfigs>();

            var config = gameConfigs.List[0];

            Assert.AreEqual("Easy", config.DifficultyName);
            Assert.AreEqual(4, config.ButtonAmount);
            Assert.AreEqual(1, config.PointPerStep);
            Assert.AreEqual(50, config.Duration);
            Assert.IsTrue(config.IsRepeating);
            Assert.AreEqual(1f, config.SpeedMultiplier);
                
        }

    }
}
