using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class ButtonUI : MonoBehaviour
    {
        public Button button;

        private bool _isClicked = false;
        
        public void OnEnable()
        {
            button.onClick.AddListener(ClickAnimation);
        }
    
        public void OnDisable()
        {
            button.onClick.RemoveListener(ClickAnimation);
        }

        [ContextMenu("TEST")]
        private void ClickAnimation()
        {
            if(!DifficultiesMenu.IsSelected)
                transform.DOScale(-.1f, .15f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        }
    }
}
