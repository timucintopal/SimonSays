using DG.Tweening;
using MoonActive.Scripts.Interface;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
    public class ScoreManager : MonoBehaviour, IConfig
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private RectTransform _barParentRect;
        private Config _currentConfig;
        
        private int _scoreAmount;
        private int ScoreAmount
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
            GameButtonManager.OnButtonMatch += Increment;
            M_StartButton.OnGameStart += Open;
            GameButtonManager.ButtonCollectEnd += Close;
            M_Timer.OnTimerFinish += SendScore;
        }
        
        void OnDisable()
        {
            M_DifficultiesMenu.OnDifficultySelect -= GetConfig;
            GameButtonManager.OnButtonMatch -= Increment;
            M_StartButton.OnGameStart -= Open;
            GameButtonManager.ButtonCollectEnd -= Close;
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
            ScoreAmount = 0;
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
