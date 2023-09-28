using System.Collections;
using System.Collections.Generic;
using MoonActive.Scripts.Controller;
using MoonActive.Scripts.ScriptableObject;
using MoonActive.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    public enum ButtonObserveState
    {
        Idle,
        Observing,
    }
    
    public class M_Button : Singleton<M_Button>
    {
        [SerializeField] ButtonData buttonData;
        
        Config CurrentConfig => M_Logic.CurrentConfig;

        int ButtonAmount => CurrentConfig.ButtonAmount;

        List<Vector3> SpawnPosList => buttonData.GetSpawnList(ButtonAmount);
        List<Color> ColorList => buttonData.ButtonSelectColors;
        List<ButtonController> _selectedButtons = new List<ButtonController>();
        
        readonly List<ButtonController> _buttonControllers = new List<ButtonController>();
        Stack<ButtonController> _buttonStack = new Stack<ButtonController>();

        public static UnityAction OnButtonsReady;
        public static UnityAction OnButtonCollect;

        WaitForSeconds _buttonSpawnDelay ;

        static ButtonObserveState _buttonObserveState;
        public static bool Status => _buttonObserveState == ButtonObserveState.Observing;

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

                buttonController.Init(i, ColorList[i]);
                
                _buttonControllers.Add(buttonController);
                
                yield return _buttonSpawnDelay;
            }
            OnButtonsReady?.Invoke();
        }

        public void Selected(ButtonController buttonController)
        {
            _selectedButtons.Add(buttonController);
            _buttonObserveState = ButtonObserveState.Idle;
        }
        
        IEnumerator ButtonSelectSequence()
        {
            _selectedButtons.Clear();
            
            Debug.Log("BUTTON SELECT ");

            // while (M_Timer.IsCounting)
            while (true)
            {
                _buttonObserveState = ButtonObserveState.Observing;
                Debug.Log("BUTTON SELECT " + _buttonObserveState);
                
                // yield return new WaitUntil(()=> (lastButtonAmount != _selectedButtons.Count));
                yield return new WaitUntil(()=> (_buttonObserveState == ButtonObserveState.Idle));

                if (!M_Timer.IsCounting)
                {
                    
                }
                else
                {
                    
                }
            }
        }
        

        ButtonController CreateButton()
        {
            var buttonController = Instantiate(buttonData.ButtonPrefab).GetComponent<ButtonController>();
            buttonController.gameObject.SetActive(false);
            return buttonController;
        }
    }
}
