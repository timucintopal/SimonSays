using System.Collections;
using MoonActive.Scripts.Class;
using MoonActive.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    //Manages PlayerName Menu Difficulty Choice Menu
    public class PreGameMenuManager : MonoBehaviour
    {
        public static UnityAction IsFirstEntry;
        public static UnityAction OnInitGame;
        public static UnityAction OnPlayerNameReady;

        static bool isWaiting = false;

        public static Config CurrentConfig;

        public static void IsDone ()
        {
            isWaiting = true;
        }

        void DifficultyReady(Config config)
        {
            CurrentConfig = config;
        
            OnInitGame?.Invoke();
        }

        WaitUntil _waitEnds;

        private void Awake()
        {
            _waitEnds = new WaitUntil(() => isWaiting);
        }

        void OnEnable()
        {
            FileReader.OnDataLoad += StartGame;
            M_DifficultiesMenu.OnDifficultySelect += DifficultyReady;
        }
    
        void OnDisable()
        {
            FileReader.OnDataLoad -= StartGame;
            M_DifficultiesMenu.OnDifficultySelect -= DifficultyReady;
        }

        void StartGame(GameConfigs arg0)
        {
            //If first entry
            if (PlayerPrefsData.GetPlayerNameStatus())
                OnPlayerNameReady?.Invoke();
            else
                StartCoroutine(GameStartFirstEntry());
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
        }

    }
}
