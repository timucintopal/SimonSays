using MoonActive.Scripts.Class;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
    public class DifficultyButton : ButtonUI
    {
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private TextMeshProUGUI durationText;
        [SerializeField] private TextMeshProUGUI repeatingText;
        [SerializeField] private TextMeshProUGUI gameSpeedText;

        Config _config;

        public Config Config
        {
            set
            {
                _config = value;

                labelText.text = _config.DifficultyName;
                pointText.text = "Earn : " + _config.PointPerStep;
                durationText.text = "Duration : " + _config.Duration;
                repeatingText.text = _config.IsRepeating ? "Repeat Mode : Yes" : "Repeat Mode : No";
                gameSpeedText.text = "Game Speed : " + _config.SpeedMultiplier + "x";
            }
        }

        public void Init(Config config, int index, UnityAction<int> callback)
        {
            Config = config;
        
            button.onClick.AddListener(()=> callback(index));
        }
    
    }
}
