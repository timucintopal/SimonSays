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
        [SerializeField] List<ButtonController> _selectedButtons = new List<ButtonController>();
        [SerializeField] List<ButtonController> _playerSelectedButtons = new List<ButtonController>();
        
        readonly List<ButtonController> _buttonControllers = new List<ButtonController>();
        Stack<ButtonController> _buttonStack = new Stack<ButtonController>();

        public static UnityAction OnButtonsReady;
        public static UnityAction OnButtonMatch;

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
        }
        
        void OnDisable()
        {
            M_Logic.OnInitGame -= InitButtons;
            M_StartButton.OnGameStart -= StartButtonSequence;
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
            Debug.Log("BUTTON SELECT STATUS " + _buttonMatch);
            
            _playerSelectedButtons.Add(buttonController);
            if (_selectedButtons.Count == _playerSelectedButtons.Count)
            {
                _buttonMatch = _selectedButtons.Last() == buttonController;
                for (var i = 0; i < _playerSelectedButtons.Count; i++)
                {
                    var playerSelectedButton = _playerSelectedButtons[i];
                    
                    if (playerSelectedButton == _selectedButtons[i]) continue;
                    
                    _buttonMatch = false;
                    break;
                }

                _buttonObserveState = ButtonObserveState.Compare;
            }
        }
        
        IEnumerator ButtonSelectSequence()
        {
            _selectedButtons.Clear();
            
            Debug.Log("BUTTON SELECT ");

            yield return new WaitUntil(() => M_Timer.IsCounting);
            while (M_Timer.IsCounting)
            {
                _playerSelectedButtons.Clear();
                Debug.Log("BUTTON START ");
                
                _selectedButtons.Add(_buttonControllers[Random.Range(0,_buttonControllers.Count)]);
                
                if(CurrentConfig.IsRepeating)
                {
                    foreach (var selectedButton in _selectedButtons)
                    {
                        selectedButton.Select();
                        yield return new WaitForSeconds(buttonData.colorDuration);
                    }
                }
                else
                {
                    _selectedButtons.Last().Select();
                    yield return new WaitForSeconds(buttonData.colorDuration);
                }
                
                _buttonObserveState = ButtonObserveState.Observing;
                // yield return new WaitUntil(()=> (lastButtonAmount != _selectedButtons.Count));
                yield return new WaitUntil(()=> (_buttonObserveState == ButtonObserveState.Compare));
                yield return new WaitForSeconds(buttonData.colorDuration);

                if (!M_Timer.IsCounting)
                {
                    
                }
                else
                {
                    if (_buttonMatch)
                    {
                        OnButtonMatch?.Invoke();
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        
        
        
        ButtonController CreateButton()
        {
            var buttonController = Instantiate(buttonData.ButtonPrefab).GetComponent<ButtonController>();
            buttonController.gameObject.SetActive(false);
            buttonController.name = "button_"+_buttonStack.Count;
            return buttonController;
        }
    }
}
