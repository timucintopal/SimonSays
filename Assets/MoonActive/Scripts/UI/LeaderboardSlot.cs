using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
    [Serializable]
    public class SlotData
    {
        public string name;
        public int score;
        public bool isPlayer = false;
    }
    
    public class LeaderboardSlot : MonoBehaviour
    {
        [SerializeField] GameObject normalBg;
        [SerializeField] GameObject playerBg;

        [Space, SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI scoreTxt;

        private RectTransform _rectTransform;
        private SlotData _slotData;
        [SerializeField] private Canvas canvas;

        public int CurrentScore => _slotData.score;
        public SlotData SlotData
        {
            get => _slotData;
            set
            {
                _slotData = value;
                
                normalBg.SetActive(!_slotData.isPlayer);
                playerBg.SetActive(_slotData.isPlayer);
                
                canvas.sortingOrder = _slotData.isPlayer ? 1 : 0;

                nameTxt.text = _slotData.name;
                scoreTxt.text = _slotData.score.ToString();
            }
        }

        public int Score
        {
            get => _slotData.score;
            set
            {
                _slotData.score = value;
                scoreTxt.text = _slotData.score.ToString();
            }
        }

        private float _width;

        private void Awake()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
            _width = Screen.width;

            _rectTransform.DOAnchorPosX(-_width, 0);
        }

        public void Init( SlotData data, Vector2 targetPos)
        {
            SlotData = data;
            _rectTransform.anchoredPosition = targetPos;
        }

        public void Close()
        {
            _rectTransform.DOAnchorPosX(_width, 1f).SetEase(Ease.InBack).OnComplete(()=> transform.localScale = Vector3.zero);
        }

        public void Open(float duration)
        {
            transform.localScale = Vector3.one;
            _rectTransform.anchoredPosition = (Vector2.right * -_width) + (Vector2.up *
                _rectTransform.anchoredPosition.y);
            _rectTransform.DOAnchorPosX(0, duration).SetEase(Ease.OutBack);
        }

        public void IncrementPoint(int incrementAmount, float duration)
        {
            DOTween.To(()=> Score, x=> Score = x, (Score + incrementAmount), duration);
        }

        public void MoveTo(Vector2 target, float duration)
        {
            _rectTransform.DOAnchorPos(target, duration);
        }
        
    }
}
