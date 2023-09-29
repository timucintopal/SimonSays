using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class DifficultyButton : ButtonUI
    {
        [SerializeField] Button _button;

        [SerializeField] TextMeshProUGUI labelText;
        [SerializeField] TextMeshProUGUI pointText;
        [SerializeField] TextMeshProUGUI durationText;
        [SerializeField] TextMeshProUGUI repeatingText;
        [SerializeField] TextMeshProUGUI gameSpeedText;

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
        
            _button.onClick.AddListener(()=> callback(index));
        }
    
    }
}
