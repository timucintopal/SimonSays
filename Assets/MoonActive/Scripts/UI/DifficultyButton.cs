using MoonActive.Scripts;
using MoonActive.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DifficultyButton : ButtonUI
{
    [SerializeField] Button _button;

    [SerializeField] TextMeshProUGUI labelText;
    [SerializeField] TextMeshProUGUI durationText;
    [SerializeField] TextMeshProUGUI repeatingText;


    Config _config;

    public Config Config
    {
        set
        {
            _config = value;

            labelText.text = _config.DifficultyName;
            durationText.text = "Duration : " + _config.Duration;
            repeatingText.text = _config.IsRepeating ? "Repeat Mode : Yes" : "Repeat Mode : No";
        }
    }

    public void Init(Config config, int index, UnityAction<int> callback)
    {
        Config = config;
        
        _button.onClick.AddListener(()=> callback(index));
    }
    
}
