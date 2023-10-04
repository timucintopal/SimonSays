using DG.Tweening;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class M_StartButton : MonoBehaviour
    {
        [SerializeField] Button _button;
        
        public static UnityAction OnGameStart;

        bool isOpen = false;

        private void Awake()
        {
            _button.onClick.AddListener(Close);
        }

        private void OnEnable()
        {
            GameButtonManager.OnButtonsReady += Open;
        }
        
        private void OnDisable()
        {
            GameButtonManager.OnButtonsReady -= Open;
        }

        [ContextMenu("Open")]
        void Open()
        {
            isOpen = true;
            transform.DOScale(Vector3.one, .75f).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                transform.DOScale(Vector3.one * 1.1f, .5f).SetLoops(-2, LoopType.Yoyo);
            });
        }

        [ContextMenu("Close")]
        void Close()
        {
            if(!isOpen) return;
            isOpen = true;
            
            transform.DOKill();
            transform.DOScale(Vector3.zero, .75f).SetEase(Ease.InBack).OnComplete(()=> OnGameStart?.Invoke());
        }
    }
}
