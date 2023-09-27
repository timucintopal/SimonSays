using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class ButtonUI : MonoBehaviour
    {
        public Button _button;

        public void OnEnable()
        {
            _button.onClick.AddListener(ClickAnimation);
        }
    
        public void OnDisable()
        {
            _button.onClick.RemoveListener(ClickAnimation);
        }

        [ContextMenu("TEST")]
        private void ClickAnimation()
        {
            transform.DOScale(-.1f, .15f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        }
    }
}
