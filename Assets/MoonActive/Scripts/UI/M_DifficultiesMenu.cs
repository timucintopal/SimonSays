using MoonActive.Scripts;
using MoonActive.Scripts.Interface;
using UnityEngine.Events;

public class M_DifficultiesMenu : PopupUI
{
    bool isSelected = false;

    public UnityAction<Difficulties> OnDifficultySelect;
    
    private void OnEnable()
    {
        M_Logic.OnPlayerNameReady += Open;
    }

    public void DifficultySelect(Difficulties difficulties)
    {
        if (isSelected) return;
        isSelected = true;
        
        OnDifficultySelect?.Invoke(difficulties);
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
