using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MoonActive.Scripts.Interface
{
    public class PopupUI : MonoBehaviour
    {
        Image _bgImg;
        Transform _menu;
        
        public void Awake()
        {
            _bgImg = transform.GetChild(0).GetComponent<Image>();
            _menu = transform.GetChild(1);
        }
        
        public void Open()
        {
            _bgImg.transform.localScale = Vector3.one;
            _bgImg.DOFade(.8f, 1);
            _menu.DOScale(Vector3.one, .5f).SetDelay(.25f);
        }

        public void Close()
        {
            _bgImg.DOFade(0, 1).OnComplete(()=> _bgImg.transform.localScale = Vector3.zero);
            _menu.DOScale(Vector3.zero,0).SetDelay(.25f);
        }
    }
}
