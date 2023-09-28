using DG.Tweening;
using MoonActive.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonActive.Scripts.Controller
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] Transform innerButton;
        [SerializeField] SpriteRenderer innerSprite;

        bool isActive = false;

        int _index = 0;

        [SerializeField] Color _selectColor;

        public void OnClick()
        {
            if(!M_Button.Status) return;
            innerButton.DOScale(-.15f, .15f).SetRelative(true).SetLoops(2, LoopType.Yoyo).OnComplete(() => Select());
            M_Button.I.Selected(this);
        }
        

        void Select(UnityAction callback = null)
        {
            Debug.Log("SELECT COLOR");
            innerSprite.DOColor(_selectColor, .5f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
              if(callback != null) callback?.Invoke();
            });
        }


        private void OnEnable()
        {
            M_Button.OnButtonCollect += Close;
        }
        
        private void OnDisable()
        {
            M_Button.OnButtonCollect -= Close;
        }

        public void Init(int index, Color color)
        {
            _index = index;
            _selectColor = color;
            transform.DOScale(Vector3.one, .75f).SetEase(Ease.OutExpo);
        }

        void Close()
        {
            transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InExpo);
        }

        public void Color()
        {
            
        }
    }
}
