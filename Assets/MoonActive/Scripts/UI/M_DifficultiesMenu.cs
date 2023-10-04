using System.Collections;
using MoonActive.Scripts.Class;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
    public class M_DifficultiesMenu : PopupUI
    {
        public static bool IsSelected = false;

        public static UnityAction<Config> OnDifficultySelect;

        GameConfigs _gameConfigs = new GameConfigs();

        [SerializeField] Transform buttonParent;
        [SerializeField] GameObject difficultyButton;
    
        private void OnEnable()
        {
            Managers.FileReader.OnDataLoad += LoadData;
            GameMenuManager.OnPlayerNameReady += Open;
            GameButtonManager.ButtonCollectEnd += Open;
        }
        
        private void OnDisable()
        {
            Managers.FileReader.OnDataLoad -= LoadData;
            GameMenuManager.OnPlayerNameReady -= Open;
            GameButtonManager.ButtonCollectEnd -= Open;
        }

        void LoadData(GameConfigs gameConfigs)
        {
            _gameConfigs = gameConfigs;
            InitMenu();
        }

        void InitMenu()
        {
            IsSelected = false;
            var buttonHeight = difficultyButton.GetComponent<RectTransform>().sizeDelta.y;

            Debug.Log("HEIGHT " + buttonHeight);

            for (var i = 0; i < _gameConfigs.List.Count; i++)
            {
                var rectTransform = Instantiate(difficultyButton, buttonParent).GetComponent<RectTransform>();
            
                rectTransform.GetComponent<DifficultyButton>().Init(_gameConfigs.List[i], i, DifficultySelect );
                rectTransform.anchoredPosition = (Vector2.down * 150)+ ( Vector2.down * buttonHeight * i ) + (Vector2.down * 25 * i) +
                                                 (Vector2.down * (buttonHeight)) ;
            }

            var parentRectTransform = buttonParent.GetComponent<RectTransform>(); 
        
            parentRectTransform.sizeDelta = Vector2.up  * buttonHeight * 1.5f;
            parentRectTransform.anchoredPosition = Vector3.zero;
        }

        public void DifficultySelect(int difficultyIndex)
        {
            if (IsSelected) return;
            IsSelected = true;

            StartCoroutine(CloseSeq(difficultyIndex));
        }

        IEnumerator CloseSeq(int difficultyIndex)
        {
            yield return new WaitForSeconds(.25f);
            Close();
            
            yield return new WaitForSeconds(.75f);
            
            OnDifficultySelect?.Invoke(_gameConfigs.List[difficultyIndex]);
            IsSelected = false;
        }
    }
}
