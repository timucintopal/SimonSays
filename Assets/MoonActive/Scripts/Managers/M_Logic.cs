using System.Collections;
using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class M_Logic : Singleton<M_Logic>
{
    public static UnityAction IsFirstEntry;
    public static UnityAction OnGameStart;

    bool isWaiting = false;

    WaitUntil _waitEnds;

    public static UnityAction OnPlayerNameReady;

    private void Awake()
    {
        _waitEnds = new WaitUntil(() => isWaiting);
    }

    void OnEnable()
    {
        M_FileReader.OnDataLoad += StartGame;
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
        
        M_PlayerName.OnNameSave += ()=> WaitStatus(true);

        yield return _waitEnds;
        M_PlayerName.OnNameSave -= ()=> WaitStatus(true);
        
        OnPlayerNameReady?.Invoke();
        
    }
}
