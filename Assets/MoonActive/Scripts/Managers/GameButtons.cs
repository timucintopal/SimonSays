using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoonActive.Scripts.Controller;
using MoonActive.Scripts.ScriptableObject;
using MoonActive.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    public enum ButtonObserveState
    {
        Compare,
        Observing,
    }
    
    public class GameButtons : Singleton<GameButtons>
    {
        [SerializeField] ButtonData buttonData;
        
        Config CurrentConfig => PreGameManager.CurrentConfig;
        int ButtonAmount => CurrentConfig.ButtonAmount;
        List<Vector3> SpawnPosList => buttonData.GetSpawnList(ButtonAmount);
        List<Color> ColorList => buttonData.ButtonSelectColors;
        
        private List<ButtonController> selectedButtons = new List<ButtonController>();
        private List<ButtonController> playerSelectedButtons = new List<ButtonController>();
        
        private List<ButtonController> _buttonControllers = new List<ButtonController>();
        private Stack<ButtonController> _buttonStack = new Stack<ButtonController>();

        public static UnityAction OnButtonsReady;
        public static UnityAction ButtonCollectStart;
        public static UnityAction ButtonCollectEnd;
        public static UnityAction OnButtonMatch;
        public static UnityAction OnButtonMatchFail;

        WaitForSeconds _buttonSpawnDelay ;

        static ButtonObserveState _buttonObserveState;
        public static bool Status => _buttonObserveState == ButtonObserveState.Observing;

        bool _buttonMatch = false;

        private void Awake()
        {
            _buttonSpawnDelay = new WaitForSeconds(buttonData.spawnDelay);
        }

        void OnEnable()
        {
            PreGameManager.OnInitGame += InitButtons;
            M_StartButton.OnGameStart += StartButtonSequence;
            M_GameEnd.OnTryAgainBtnClick += CollectButtons;
            M_GameEnd.OnNextBtnClick += CollectButtons;
        }
        
        void OnDisable()
        {
            PreGameManager.OnInitGame -= InitButtons;
            M_StartButton.OnGameStart -= StartButtonSequence;
            M_GameEnd.OnTryAgainBtnClick -= CollectButtons;
            M_GameEnd.OnNextBtnClick -= CollectButtons;
        }

        void InitButtons()
        {
            StartCoroutine(InitButtonSequence());
        }

        void StartButtonSequence()
        {
            StartCoroutine(ButtonSelectSequence());
        }

        IEnumerator InitButtonSequence()
        {
            if (_buttonStack.Count < ButtonAmount)
            {
                var stackAmount = _buttonStack.Count;
                for (int i = 0; i < ButtonAmount - stackAmount; i++)
                    _buttonStack.Push(CreateButton());
            }
            
            ColorList.Shuffle();
            
            for (int i = 0; i < ButtonAmount; i++)
            {
                var buttonController = _buttonStack.Pop();
                buttonController.gameObject.SetActive(true);
                buttonController.transform.position = SpawnPosList[i];
                buttonController.Init(i, ColorList[i], buttonData.colorDuration, CurrentConfig.SpeedMultiplier);
                
                _buttonControllers.Add(buttonController);
                
                yield return _buttonSpawnDelay;
            }
            OnButtonsReady?.Invoke();
        }

        public void Selected(ButtonController buttonController)
        {
            if(buttonController == selectedButtons[playerSelectedButtons.Count])
            {
                playerSelectedButtons.Add(buttonController);

                if (selectedButtons.Count == playerSelectedButtons.Count)
                {
                    _buttonMatch = true;
                    _buttonObserveState = ButtonObserveState.Compare;
                }
            }
            else
            {
                _buttonMatch = false;
                _buttonObserveState = ButtonObserveState.Compare;
            }
        }
        
        IEnumerator ButtonSelectSequence()
        {
            selectedButtons.Clear();
            
            yield return new WaitUntil(() => M_Timer.IsCounting);
            yield return new WaitForSeconds(1);
            while (M_Timer.IsCounting)
            {
                playerSelectedButtons.Clear();
                selectedButtons.Add(_buttonControllers[Random.Range(0,_buttonControllers.Count)]);
                
                if(CurrentConfig.IsRepeating)
                {
                    foreach (var selectedButton in selectedButtons)
                    {
                        selectedButton.Select();
                        yield return new WaitForSeconds((buttonData.colorDuration + .15f) / CurrentConfig.SpeedMultiplier);
                    }
                }
                else
                {
                    selectedButtons.Last().Select();
                    yield return new WaitForSeconds((buttonData.colorDuration + .15f) / CurrentConfig.SpeedMultiplier);
                }
                
                _buttonObserveState = ButtonObserveState.Observing;
                yield return new WaitUntil(()=> (_buttonObserveState == ButtonObserveState.Compare));
                yield return new WaitForSeconds((buttonData.colorDuration + .25f) / CurrentConfig.SpeedMultiplier );

                if (!M_Timer.IsCounting)
                    yield break;
                
                if (_buttonMatch)
                    OnButtonMatch?.Invoke();
                else
                    OnButtonMatchFail?.Invoke();
            }
        }
        
        ButtonController CreateButton()
        {
            var buttonController = Instantiate(buttonData.ButtonPrefab,transform).GetComponent<ButtonController>();
            buttonController.gameObject.SetActive(false);
            buttonController.name = "button_"+_buttonStack.Count;
            return buttonController;
        }

        void CollectButtons()
        {
            ButtonCollectStart?.Invoke();
            
            foreach (var buttonController in _buttonControllers)
                _buttonStack.Push(buttonController);
            
            _buttonControllers.Clear();

            StartCoroutine(Helper.InvokeAction(()=>ButtonCollectEnd?.Invoke(), 1));
        }
    }
}