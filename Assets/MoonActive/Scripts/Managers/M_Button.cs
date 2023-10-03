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
        Show,
        Idle,
        Compare,
        Observing,
    }
    
    public class M_Button : Singleton<M_Button>
    {
        [SerializeField] ButtonData buttonData;
        
        Config CurrentConfig => M_Logic.CurrentConfig;
        int ButtonAmount => CurrentConfig.ButtonAmount;
        List<Vector3> SpawnPosList => buttonData.GetSpawnList(ButtonAmount);
        List<Color> ColorList => buttonData.ButtonSelectColors;
        
        [SerializeField] List<ButtonController> selectedButtons = new List<ButtonController>();
        [SerializeField] List<ButtonController> playerSelectedButtons = new List<ButtonController>();
        
        readonly List<ButtonController> _buttonControllers = new List<ButtonController>();
        readonly Stack<ButtonController> _buttonStack = new Stack<ButtonController>();

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
            M_Logic.OnInitGame += InitButtons;
            M_StartButton.OnGameStart += StartButtonSequence;
            GameEnd.OnTryAgainBtnClick += CollectButtons;
        }
        
        void OnDisable()
        {
            M_Logic.OnInitGame -= InitButtons;
            M_StartButton.OnGameStart -= StartButtonSequence;
            GameEnd.OnTryAgainBtnClick -= CollectButtons;
        }

        void InitButtons()
        {
            StartCoroutine(InitButtonSequence());
        }

        void StartButtonSequence()
        {
            Debug.Log("START SEQ");
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
            
            // if (selectedButtons.Count == playerSelectedButtons.Count)
            // {
            //     _buttonMatch = selectedButtons.Last() == buttonController;
            //     for (var i = 0; i < playerSelectedButtons.Count; i++)
            //     {
            //         var playerSelectedButton = playerSelectedButtons[i];
            //         
            //         if (playerSelectedButton == selectedButtons[i]) continue;
            //         
            //         _buttonMatch = false;
            //         break;
            //     }
            //     Debug.Log("BUTTON SELECT STATUS " + _buttonMatch);
            //     _buttonObserveState = ButtonObserveState.Compare;
            // }
        }
        
        IEnumerator ButtonSelectSequence()
        {
            selectedButtons.Clear();
            
            yield return new WaitUntil(() => M_Timer.IsCounting);
            yield return new WaitForSeconds(1);
            while (M_Timer.IsCounting)
            {
                playerSelectedButtons.Clear();
                Debug.Log("BUTTON START ");
                
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
                // yield return new WaitUntil(()=> (lastButtonAmount != _selectedButtons.Count));
                yield return new WaitUntil(()=> (_buttonObserveState == ButtonObserveState.Compare));
                yield return new WaitForSeconds((buttonData.colorDuration + .25f) / CurrentConfig.SpeedMultiplier );

                if (!M_Timer.IsCounting)
                {
                    Debug.Log("BUTTON SEQ ENDED TIMER");
                    yield break;
                }
                
                if (_buttonMatch)
                    OnButtonMatch?.Invoke();
                else
                    OnButtonMatchFail?.Invoke();
            }
            Debug.Log("BUTTON SEQ ENDED");
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
