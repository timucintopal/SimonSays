using System;
using DG.Tweening;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
    public class M_Score : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        int _scoreAmount;

        private RectTransform _barParentRect;
        
        public int ScoreAmount
        {
            get => _scoreAmount;
            set
            {
                _scoreAmount = value;
                scoreText.text = _scoreAmount.ToString();
            }
        }

        private void Awake()
        {
            _barParentRect = transform.GetChild(0).GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            M_Button.OnButtonMatch += Increment;
            M_StartButton.OnGameStart += Open;
        }
        
        void OnDisable()
        {
            M_Button.OnButtonMatch -= Increment;
            M_StartButton.OnGameStart -= Open;
        }

        private void Open()
        {
            _barParentRect.DOAnchorPosY(-300, .5f).SetEase(Ease.OutExpo);
        }

        public void Reset()
        {
            ScoreAmount = 0;
        }

        public void Increment()
        {
            ScoreAmount++;
        }


    }
}
