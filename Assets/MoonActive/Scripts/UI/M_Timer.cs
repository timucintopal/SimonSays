using DG.Tweening;
using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using TMPro;
using UnityEngine;

public class M_Timer : MonoBehaviour
{
    Config CurrentConfig => M_Logic.CurrentConfig;
    
    [SerializeField] TextMeshProUGUI _timerText;
    RectTransform _rectTransform;

    public static bool IsCounting => _isCounting;
    static readonly bool _isCounting = false;
    float _timer;
    
    private float Timer
    {
        get => _timer;
        set
        {
            _timer = value;

            _timerText.text = string.Format("{0:00}:{1:00}:", Mathf.FloorToInt(_timer / 60),
                Mathf.FloorToInt(_timer % 60));
        }
    }

    
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = Vector2.up * 100;
    }

    private void OnEnable()
    {
        M_Logic.OnInitGame += InitTimer;
    }
    
    private void OnDisable()
    {
        M_Logic.OnInitGame -= InitTimer;
    }

    private void InitTimer()
    {
        
    }

    void Open()
    {
        Timer = CurrentConfig.Duration;
        _rectTransform.DOAnchorPosY(-200, .5f).SetEase(Ease.OutExpo);
    }
    
    void Close()
    {
        
    }

    void Update()
    {
        if (_isCounting)
            Timer -= Time.deltaTime;
    }
}
