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

        bool _isActive = false;
        int _index = 0;
        float _colorDuration;
        float _speedMultiplier = 1;

        [SerializeField] Color selectColor;
        
        private void OnEnable()
        {
            M_Button.ButtonCollectStart += Close;
        }
        
        private void OnDisable()
        {
            M_Button.ButtonCollectStart -= Close;
        }
        
        public void OnClick()
        {
            if(!M_Button.Status) return;
            innerButton.DOScale(-.05f, .05f).SetRelative(true).SetLoops(2, LoopType.Yoyo);//.OnComplete();
            Select();
            M_Button.I.Selected(this);
        }
        
        public void Select(UnityAction callback = null)
        {
            innerSprite.DOComplete();
            Debug.Log("SELECT COLOR " + name);
            innerSprite.DOColor(selectColor, _colorDuration / _speedMultiplier).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
              if(callback != null) callback?.Invoke();
            });
            M_Audio.I.PlaySound(_index);
            
        }

        public void Init(int index, Color color, float buttonDataColorDuration, float currentConfigSpeedMultiplier)
        {
            _colorDuration = buttonDataColorDuration;
            _speedMultiplier = currentConfigSpeedMultiplier;
            _index = index;
            selectColor = color;
            transform.DOScale(Vector3.one, .3f).SetEase(Ease.OutExpo);
        }

        void Close()
        {
            transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InExpo).OnComplete(()=> gameObject.SetActive(false));
        }

        public void Color()
        {
            
        }
    }
}
