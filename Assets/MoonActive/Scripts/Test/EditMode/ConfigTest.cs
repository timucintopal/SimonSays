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
            // var gameConfigs = DataManager.LoadDataJson<GameConfigs>();

            var config = gameConfigs.List[0];

            Assert.AreEqual("Easy", config.DifficultyName);
            Assert.AreEqual(4, config.ButtonAmount);
            Assert.AreEqual(1, config.PointPerStep);
            Assert.AreEqual(50, config.Duration);
            Assert.IsTrue(config.IsRepeating);
            Assert.AreEqual(1f, config.SpeedMultiplier);
            
            config = gameConfigs.List[1];
            
            Assert.AreEqual("Medium", config.DifficultyName);
            Assert.AreEqual(5, config.ButtonAmount);
            Assert.AreEqual(2, config.PointPerStep);
            Assert.AreEqual(45, config.Duration);
            Assert.IsTrue(config.IsRepeating);
            Assert.AreEqual(1.25f, config.SpeedMultiplier);
            
            config = gameConfigs.List[2];
            
            Assert.AreEqual("Hard", config.DifficultyName);
            Assert.AreEqual(6, config.ButtonAmount);
            Assert.AreEqual(3, config.PointPerStep);
            Assert.AreEqual(30, config.Duration);
            Assert.IsFalse(config.IsRepeating);
            Assert.AreEqual(1.5f, config.SpeedMultiplier);
                
        }

    }
}
