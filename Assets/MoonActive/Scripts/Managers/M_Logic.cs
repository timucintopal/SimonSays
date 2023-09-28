using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    public class M_Logic : Singleton<M_Logic>
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
}
