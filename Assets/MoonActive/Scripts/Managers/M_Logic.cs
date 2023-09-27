using System.Collections;
using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using MoonActive.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

public class M_Logic : Singleton<M_Logic>
{
    public static UnityAction IsFirstEntry;
    public static UnityAction OnGameStart;
    public static UnityAction OnPlayerNameReady;

    static bool isWaiting = false;

    public static void IsDone ()
    {
        isWaiting = true;
    }

    void DifficultyReady(Config _config)
    {
        
    }

    WaitUntil _waitEnds;

    private void Awake()
    {
        _waitEnds = new WaitUntil(() => isWaiting);
    }

    void OnEnable()
    {
        M_FileReader.OnDataLoad += StartGame;
        M_DifficultiesMenu.OnDifficultySelect += DifficultyReady;
    }
    
    void OnDisable()
    {
        M_FileReader.OnDataLoad -= StartGame;
        M_DifficultiesMenu.OnDifficultySelect -= DifficultyReady;
    }

    void StartGame(GameConfigs arg0)
    {
        if (M_PlayerPrefs.CheckFirstEntry())
            StartCoroutine(GameStartFirstEntry());
        else
            OnPlayerNameReady?.Invoke();
    }

    void WaitStatus(bool newStatus)
    {
        isWaiting = newStatus;
    }
    
    IEnumerator GameStartFirstEntry()
    {
        yield return new WaitForSeconds(1);
        IsFirstEntry?.Invoke();

        yield return _waitEnds;
        WaitStatus(false);
        OnPlayerNameReady?.Invoke();

        // yield return _waitEnds;
    }
}
