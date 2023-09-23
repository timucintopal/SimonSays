using System;
using System.Collections;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class M_Logic : MonoBehaviour
{
    public static UnityAction IsFirstEntry;
    public static UnityAction OnGameStart;

    bool isWaiting = false;

    WaitUntil _waitEnds;


    private void Awake()
    {
        _waitEnds = new WaitUntil(() => isWaiting);
    }

    void WaitStatus(bool newStatus)
    {
        isWaiting = newStatus;
    }
    
    void Start()
    {
        if (M_PlayerPrefs.CheckFirstEntry())
            StartCoroutine(GameStartFirstEntry());
        else
        {
            
        }
    }
    
    IEnumerator GameStartFirstEntry()
    {
        yield return new WaitForSeconds(1);
        IsFirstEntry?.Invoke();
        M_PlayerName.OnNameSave += ()=> WaitStatus(true);

        yield return _waitEnds;
        M_PlayerName.OnNameSave -= ()=> WaitStatus(true);
    }
}
