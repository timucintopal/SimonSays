using MoonActive.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    [SerializeField] Button _button;
    
    Config _config;
    public Config Config
    {
        get => _config;
        set
        {
            _config = value;
        }
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        // M_DifficultiesMenu.I.DifficultySelect(Config.DifficultyName);
    }
}
