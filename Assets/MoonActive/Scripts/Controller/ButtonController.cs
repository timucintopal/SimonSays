using DG.Tweening;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Controller
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] Transform innerButton;
        [SerializeField] SpriteRenderer innerSprite;

        bool isActive = false;
        int _index = 0;
        float _colorDuration;
        float _speedMultiplier = 1;

        [SerializeField] Color selectColor;

        public void OnClick()
        {
            if(!M_Button.Status) return;
            innerButton.DOScale(-.15f, .15f).SetRelative(true).SetLoops(2, LoopType.Yoyo).OnComplete(() => Select());
            M_Button.I.Selected(this);
        }
        

        public void Select(UnityAction callback = null)
        {
            innerSprite.DOComplete();
            Debug.Log("SELECT COLOR " + name);
            innerSprite.DOColor(selectColor, _colorDuration/2 * _speedMultiplier).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
              if(callback != null) callback?.Invoke();
            });
        }

        // private void OnEnable()
        // {
        //     M_Button.OnButtonCollect += Close;
        // }
        //
        // private void OnDisable()
        // {
        //     M_Button.OnButtonCollect -= Close;
        // }

        public void Init(int index, Color color, float buttonDataColorDuration, float currentConfigSpeedMultiplier)
        {
            _colorDuration = buttonDataColorDuration;
            // _speedMultiplier = currentConfigSpeedMultiplier;
            _index = index;
            selectColor = color;
            transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutExpo);
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
