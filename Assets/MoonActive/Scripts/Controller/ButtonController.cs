using DG.Tweening;
using UnityEngine;

namespace MoonActive.Scripts.Controller
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Transform InnerButton;

        public void OnClick()
        {
            InnerButton.DOScale(-.15f, .15f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
        }
        
    }
}
