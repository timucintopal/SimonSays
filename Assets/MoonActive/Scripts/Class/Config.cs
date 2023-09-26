using System;
using System.Collections.Generic;

namespace MoonActive.Scripts
{
    [Serializable]
    public class GameConfigs
    {
        public List<Config> List;
    }
    
    [Serializable]
    public class Config
    {
        public string DifficultyName;
        public int ButtonAmount;
        public int PointPerStep;
        public int Duration;
        public bool IsRepeating = false;
        public float SpeedMultiplier = 1;
    }
}
