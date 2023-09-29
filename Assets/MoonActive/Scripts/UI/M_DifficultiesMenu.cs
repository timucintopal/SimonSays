using System.Collections;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
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
            isSelected = false;
            var buttonHeight = DifficultyButton.GetComponent<RectTransform>().sizeDelta.y;

            Debug.Log("HEIGHT " + buttonHeight);

            for (var i = 0; i < _gameConfigs.List.Count; i++)
            {
                var rectTransform = Instantiate(DifficultyButton, ButtonParent).GetComponent<RectTransform>();
            
                rectTransform.GetComponent<DifficultyButton>().Init(_gameConfigs.List[i], i, DifficultySelect );
                rectTransform.anchoredPosition = (Vector2.down * 150)+ ( Vector2.down * buttonHeight * i ) + (Vector2.down * 25 * i) +
                                                 (Vector2.down * (buttonHeight)) ;
            }

            var parentRectTransform = ButtonParent.GetComponent<RectTransform>(); 
        
            parentRectTransform.sizeDelta = Vector2.up  * buttonHeight * 1.5f;
            parentRectTransform.anchoredPosition = Vector3.zero;
        }

        public void DifficultySelect(int difficultyIndex)
        {
            if (isSelected) return;
            isSelected = true;

            StartCoroutine(CloseSeq(difficultyIndex));
        }

        IEnumerator CloseSeq(int difficultyIndex)
        {
            yield return new WaitForSeconds(.25f);
            Close();
            
            yield return new WaitForSeconds(.75f);
            
            OnDifficultySelect?.Invoke(_gameConfigs.List[difficultyIndex]);
        }
    }
}
