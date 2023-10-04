using System;
using System.Collections;
using DG.Tweening;
using MoonActive.Scripts.Class;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MoonActive.Scripts.UI
{
    public class PlayerNameMenuUI : MonoBehaviour
    {
        [SerializeField] private Image _bgImg;
        [SerializeField] private Transform _menu;
        private RectTransform _menuRectTransform;

        [Space, SerializeField] private Button _enterButton;
        [SerializeField] private TMP_InputField _nameField;
        [SerializeField] private TextMeshProUGUI _placeholderField;

        bool _isSaved = false;

        private void Awake()
        {
            if (_bgImg == null) transform.GetChild(0).GetComponent<Image>();
            if (!_menu) transform.GetChild(1);
            _menuRectTransform = _menu.GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            M_Logic.IsFirstEntry += Open;
            _enterButton.onClick.AddListener(TrySaveName);
        }
    
        void OnDisable()
        {
            M_Logic.IsFirstEntry -= Open;
            _enterButton.onClick.RemoveListener(TrySaveName);
        }

        void TrySaveName()
        {
            if(_isSaved) return;

            if (CheckName())
            {
                PlayerPrefsData.SetPlayerName(_nameField.text);
                
                _nameField.text = "";
                _placeholderField.text = "Saved!";
                _isSaved = true;
                StartCoroutine(Close());
            }
            else
                RejectShake();
        }

        bool CheckName()
        {
            var tempName = _nameField.text;
            if (String.Compare(tempName, "") == 0) return false;
            if (String.Compare(tempName, " ") == 0) return false;

            foreach (char letter in tempName)
            {
                if (char.IsWhiteSpace(letter))
                    return false;
            }
            return true;
        }

        void Open()
        {
            _bgImg.transform.localScale = Vector3.one;
            _bgImg.DOFade(.8f, .5f);

            var anchoredPos = _menuRectTransform.anchoredPosition;
            _menuRectTransform.anchoredPosition = new Vector2(-Screen.width - 200, anchoredPos.y);
            _menu.localScale = Vector3.one;
            _menuRectTransform.DOAnchorPosX(0, .75f).SetEase(Ease.OutExpo);
        }
    
        IEnumerator Close()
        {
            yield return new WaitForSeconds(.5f);
            M_Logic.IsDone();
            _bgImg.DOFade(0, .5f);
            _menuRectTransform.DOAnchorPosX(Screen.width + 200, .75f).SetEase(Ease.InExpo).OnComplete(()=> gameObject.SetActive(false));
        }

        void RejectShake()
        {
            _enterButton.DOKill();
            _enterButton.transform.DOPunchRotation(Vector3.forward * 45, .25f, 20, 0).SetEase(Ease.OutQuad);
        }
    }
}
