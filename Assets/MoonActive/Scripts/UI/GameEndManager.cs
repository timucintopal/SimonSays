using System.Collections.Generic;
using DG.Tweening;
using MoonActive.Scripts.Class;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class GameEndManager : PopupUI
    {
        [SerializeField] private Transform failTextTransform;
        [SerializeField] private Transform timesUpTextTransform;
        [SerializeField] private TextMeshProUGUI timesUpText;

        [SerializeField] private Button tryAgainButton;
        [SerializeField] private Button nextButton;
        
        public static UnityAction OnNextBtnClick;
        public static UnityAction OnTryAgainBtnClick;
        
        //For future usages, maybe if needed
        public static UnityAction OnGameSuccess;
        public static UnityAction OnGameFail;
        public static UnityAction OnLevelEnd;

        private Stack<Transform> _openedTransforms = new Stack<Transform>();

        bool isFailed;

        private void OnEnable()
        {
            TimerManager.OnTimerFinish += SuccessGame;
            GameButtonManager.OnButtonMatchFail += FailGame;
            GameButtonManager.ButtonCollectEnd += CloseAllObject;
            LeaderboardManager.OnLeaderboardSeqEnd += OpenNextButton;
            
            tryAgainButton.onClick.AddListener(()=>Invoke(OnTryAgainBtnClick));
            nextButton.onClick.AddListener(()=>Invoke(OnNextBtnClick));
        }
        
        private void OnDisable()
        {
            TimerManager.OnTimerFinish -= SuccessGame;
            GameButtonManager.OnButtonMatchFail -= FailGame;
            GameButtonManager.ButtonCollectEnd -= CloseAllObject;
            LeaderboardManager.OnLeaderboardSeqEnd -= OpenNextButton;
            
            tryAgainButton.onClick.RemoveListener(()=>Invoke(OnTryAgainBtnClick));
            nextButton.onClick.RemoveListener(()=>Invoke(OnNextBtnClick));
        }
        
        void SuccessGame()
        {
            timesUpText.DOFade(1, 0);
            OpenObject(timesUpTextTransform, true, timesUpText);
            OnGameSuccess?.Invoke();
            OnLevelEnd?.Invoke();
        }

        void FailGame()
        {
            OpenObject(failTextTransform);
            StartCoroutine(Helper.InvokeAction(()=> OpenObject(tryAgainButton.transform),1));
            OnGameFail?.Invoke();
            OnLevelEnd?.Invoke();
        }

        void OpenObject(Transform target, bool autoClose = false, TextMeshProUGUI text = null)
        {
            if(autoClose)
                if (text)
                    text.DOFade(0, 1).SetDelay(1);
            
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

        void OpenNextButton()
        {
            OpenObject(nextButton.transform);
        }

        void Invoke(UnityAction callback)
        {
            callback?.Invoke();
        }
    }
}
