using System.Collections.Generic;
using DG.Tweening;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class GameEnd : PopupUI
    {
        [SerializeField] Transform failTextTransform;
        [SerializeField] Transform timesUpTextTransform;

        [SerializeField] Button TryAgainButton;
        [SerializeField] Button NextButton;
        
        public static UnityAction OnNextBtnClick;
        public static UnityAction OnTryAgainBtnClick;

        public static UnityAction OnGameSuccess;
        public static UnityAction OnGameFail;
        public static UnityAction OnLevelEnd;

        private Stack<Transform> _openedTransforms = new Stack<Transform>();

        bool isFailed;

        private void OnEnable()
        {
            M_Timer.OnTimerFinish += SuccessGame;
            M_Button.OnButtonMatchFail += FailGame;
            M_Button.ButtonCollectEnd += CloseAllObject;
            
            TryAgainButton.onClick.AddListener(()=>Invoke(OnTryAgainBtnClick));
            NextButton.onClick.AddListener(()=>Invoke(OnNextBtnClick));
        }
        
        private void OnDisable()
        {
            M_Timer.OnTimerFinish -= SuccessGame;
            M_Button.OnButtonMatchFail -= FailGame;
            M_Button.ButtonCollectEnd -= CloseAllObject;
            
            TryAgainButton.onClick.RemoveListener(()=>Invoke(OnTryAgainBtnClick));
            NextButton.onClick.RemoveListener(()=>Invoke(OnNextBtnClick));
        }
        
        void SuccessGame()
        {
            OpenObject(timesUpTextTransform);
            OnGameSuccess?.Invoke();
            OnLevelEnd?.Invoke();
        }

        void FailGame()
        {
            OpenObject(failTextTransform);
            StartCoroutine(Helper.InvokeAction(()=> OpenObject(TryAgainButton.transform),1));
            OnGameFail?.Invoke();
            OnLevelEnd?.Invoke();
        }

        void OpenObject(Transform target)
        {
            target.DOScale(Vector3.one, 1).SetEase(Ease.OutExpo);
            _openedTransforms.Push(target);
        }
        
        void CloseAllObject()
        {
            var loopAmount = _openedTransforms.Count;
            for (int i = 0; i < loopAmount; i++)
            {
                var openedTransform = _openedTransforms.Pop();
                
                openedTransform.DOKill();
                openedTransform.DOScale(Vector3.zero, .5f).SetEase(Ease.InExpo);
            }
        }

        void Invoke(UnityAction callback)
        {
            callback?.Invoke();
        }
    }
}
