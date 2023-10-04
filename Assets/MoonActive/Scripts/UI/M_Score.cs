using DG.Tweening;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
    public class M_Score : MonoBehaviour, IConfig
    {
        [SerializeField] TextMeshProUGUI scoreText;
        int _scoreAmount;

        private RectTransform _barParentRect;

        private Config _currentConfig;
        
        public int ScoreAmount
        {
            get => _scoreAmount;
            set
            {
                _scoreAmount = Mathf.Clamp(value,0, 99999999);
                scoreText.text = _scoreAmount.ToString();
            }
        }

        public static UnityAction<int> GameEndScore;

        private void Awake()
        {
            _barParentRect = transform.GetChild(0).GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            M_DifficultiesMenu.OnDifficultySelect += GetConfig;
            GameButtons.OnButtonMatch += Increment;
            M_StartButton.OnGameStart += Open;
            GameButtons.ButtonCollectEnd += Close;
            M_Timer.OnTimerFinish += SendScore;
        }
        
        void OnDisable()
        {
            M_DifficultiesMenu.OnDifficultySelect -= GetConfig;
            GameButtons.OnButtonMatch -= Increment;
            M_StartButton.OnGameStart -= Open;
            GameButtons.ButtonCollectEnd -= Close;
            M_Timer.OnTimerFinish -= SendScore;
        }

        void SendScore()
        {
            GameEndScore?.Invoke(ScoreAmount);
        }

        public void GetConfig(Config config)
        {
            _currentConfig = config;
        }

        private void Open()
        {
            _barParentRect.DOAnchorPosY(-300, .5f).SetEase(Ease.OutExpo);
        }
        
        private void Close()
        {
            _barParentRect.DOAnchorPosY(200, .5f).SetEase(Ease.OutExpo);
        }

        public void Reset()
        {
            ScoreAmount = 0;
        }

        public void Increment()
        {
            ScoreAmount += _currentConfig.PointPerStep;
        }


        
    }
}
