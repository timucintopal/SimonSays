using MoonActive.Scripts;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class M_DifficultiesMenu : PopupUI
{
    bool isSelected = false;

    public static UnityAction<Config> OnDifficultySelect;

    GameConfigs _gameConfigs = new GameConfigs();

    [SerializeField] Transform ButtonParent;
    [SerializeField] GameObject DifficultyButton;
    
    private void OnEnable()
    {
        M_FileReader.OnDataLoad += LoadData;
        M_Logic.OnPlayerNameReady += Open;
    }

    void LoadData(GameConfigs gameConfigs)
    {
        _gameConfigs = gameConfigs;
        InitMenu();
    }

    void InitMenu()
    {
        var buttonHeight = DifficultyButton.GetComponent<RectTransform>().sizeDelta.y;

        Debug.Log("HEIGHT " + buttonHeight);

        for (var i = 0; i < _gameConfigs.List.Count; i++)
        {
            var rectTransform = Instantiate(DifficultyButton, ButtonParent).GetComponent<RectTransform>();
            
            rectTransform.GetComponent<DifficultyButton>().Init(_gameConfigs.List[i], i, DifficultySelect );
            rectTransform.anchoredPosition = Vector2.zero + ( Vector2.down * buttonHeight * i ) + (Vector2.down * 100 * i) +
                                             (Vector2.down * (buttonHeight)) ;
        }

        var parentRectTransform = ButtonParent.GetComponent<RectTransform>(); 
        
        parentRectTransform.sizeDelta = Vector2.up  * buttonHeight * 1.5f;
        parentRectTransform.anchoredPosition = Vector3.zero;
    }

    public void DifficultySelect(int difficultyIndex)
    {
        Debug.Log("SELECTION MADE BY " + difficultyIndex);
        if (isSelected) return;
        isSelected = true;
        
        OnDifficultySelect?.Invoke(_gameConfigs.List[difficultyIndex]);
        Close();
    }
    
    #region Singleton
    
    private static object _lock = new object();
    private static M_DifficultiesMenu _instance;

    public static M_DifficultiesMenu I
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null) _instance = (M_DifficultiesMenu) FindObjectOfType(typeof(M_DifficultiesMenu));

                return _instance;
            }
        }
    }
    
    #endregion


}
