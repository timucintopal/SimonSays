using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using UnityEngine;

public class M_Game : MonoBehaviour
{
    Config CurrentConfig => M_Logic.CurrentConfig;

    
    private void OnEnable()
    {
        M_Button.OnButtonsReady += StartGame;
    }
        
    private void OnDisable()
    {
        M_Button.OnButtonsReady -= StartGame;
    }

    void StartGame()
    {
        
    }
    
}
