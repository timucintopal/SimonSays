using System;
using System.Collections.Generic;
using MoonActive.Scripts.Controller;
using MoonActive.Scripts.ScriptableObject;
using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class M_Button : MonoBehaviour
    {
        [SerializeField] ButtonData buttonData;
        
        Config _currentConfig;

        int ButtonAmount => _currentConfig.ButtonAmount;

        List<Vector3> SpawnPosList => buttonData.GetSpawnList(ButtonAmount);

        readonly List<ButtonController> _buttonControllers = new List<ButtonController>();
        Stack<ButtonController> _buttonStack = new Stack<ButtonController>();

        private void OnEnable()
        {
            M_Logic.OnGameStart += InitButtons;
        }
        
        private void OnDisable()
        {
            M_Logic.OnGameStart -= InitButtons;
        }

        void InitButtons(Config config)
        {
            _currentConfig = config;
            
            Debug.Log("STACK " + _buttonStack.Count + " - " + ButtonAmount);

            if (_buttonStack.Count < ButtonAmount)
            {
                var stackAmount = _buttonStack.Count;
                Debug.Log("STACK EMPTY");
                for (int i = 0; i < ButtonAmount - stackAmount; i++)
                    _buttonStack.Push(Instantiate(buttonData.ButtonPrefab).GetComponent<ButtonController>());
                Debug.Log("STACK Full " + _buttonStack.Count);
            }
        
            for (int i = 0; i < ButtonAmount; i++)
            {
                Debug.Log("STACK Pull");
                var button = _buttonStack.Pop();
                _buttonControllers.Add(button);

                button.transform.position = SpawnPosList[i];
            }
        
            
        }
    }
}
