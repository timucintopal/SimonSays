using System;
using System.Collections.Generic;

namespace MoonActive.Scripts
{
    [Serializable]
    public enum Difficulties
    {
        Easy,
        Medium,
        Hard
    }
    
    [Serializable]
    public class GameConfigs
    {
        public List<Config> List;
    }
    
    [Serializable]
    public class Config
    {
        public Difficulties Difficulty;
        public int ButtonAmount;
        public int PointPerStep;
        public int Duration;
        public bool IsRepeating = false;
        public float SpeedMultiplier = 1;
    }
}