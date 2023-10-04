using System.Collections;
using System.Linq;
using DG.Tweening;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.UI
{
    public class TimerManager : MonoBehaviour
    {
        private Config CurrentConfig => GameMenuManager.CurrentConfig;
    
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI preGameTimerText;
        RectTransform _timerBarRect;

        string[] _preGamePhrases = new string[]{"3","2","1","Go!"}; 

        public static bool IsCounting => _isCounting;
        
        static bool _isCounting = false;
        
        public static UnityAction OnTimerFinish;
        
        float _timer;
        Transform _timerBarParent;
    
        private float Timer
        {
            get => _timer;
            set
            {
                _timer = Mathf.Clamp(value,0,99999);

                timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(_timer / 60),
                    Mathf.FloorToInt(_timer % 60));
            }
        }
    
        private void Awake()
        {
            _timerBarParent = transform.GetChild(0);
            
            _timerBarRect = _timerBarParent.GetComponent<RectTransform>();
            _timerBarRect.anchoredPosition = Vector2.up * 100;
        }

        private void OnEnable()
        {
            GameStartButton.OnGameStart += InitTimer;
            GameButtonManager.OnButtonMatchFail += StopTimer;
            GameButtonManager.ButtonCollectEnd += Close;
        }
    
        private void OnDisable()
        {
            GameStartButton.OnGameStart -= InitTimer;
            GameButtonManager.OnButtonMatchFail -= StopTimer;
            GameButtonManager.ButtonCollectEnd -= Close;
        }

        private void InitTimer()
        {
            StartCoroutine(Open());
        }

        void StopTimer()
        {
            _isCounting = false;
        }

        IEnumerator Open()
        {
            Timer = CurrentConfig.Duration;
            _timerBarRect.DOAnchorPosY(-200, .5f).SetEase(Ease.OutExpo);

            preGameTimerText.DOFade(0, 0);
            preGameTimerText.rectTransform.localScale = Vector3.zero;
            
            for (int i = 0; i < 3; i++)
            {
                preGameTimerText.text = _preGamePhrases[i];
                preGameTimerText.rectTransform.DOScale(Vector3.one, .45f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine);
                preGameTimerText.DOFade(.8f, .45f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(1);
            }
            preGameTimerText.text = _preGamePhrases.Last();
            preGameTimerText.rectTransform.DOScale(Vector3.one, .45f).SetEase(Ease.OutSine);
            preGameTimerText.DOFade(.8f, .6f).SetLoops(2, LoopType.Yoyo);
            _isCounting = true;
        }
    
        void Close()
        {
            _timerBarRect.DOAnchorPosY(100, .5f).SetEase(Ease.InExpo);
        }

        void Update()
        {
            if (_isCounting)
            {
                Timer -= Time.deltaTime;
                if (Timer == 0)
                {
                    _isCounting = false;
                    OnTimerFinish?.Invoke();
                }
            }
        }
    }
}
