using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using UnityEngine;

public class M_Game : MonoBehaviour
{
    Config CurrentConfig => GameMenuManager.CurrentConfig;

    
    private void OnEnable()
    {
        GameButtons.OnButtonsReady += StartGame;
    }
        
    private void OnDisable()
    {
        GameButtons.OnButtonsReady -= StartGame;
    }

    void StartGame()
    {
        
    }
    
}
