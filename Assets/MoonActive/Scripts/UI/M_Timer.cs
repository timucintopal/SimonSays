using System;
using DG.Tweening;
using MoonActive.Scripts;
using TMPro;
using UnityEngine;

public class M_Timer : MonoBehaviour
{
    Config _currentConfig;
    
    [SerializeField] TextMeshProUGUI _timerText;
    RectTransform _rectTransform;

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


    bool _isCounting = false;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = Vector2.up * 100;
    }

    private void OnEnable()
    {
        M_Logic.OnGameStart += InitTimer;
    }
    
    private void OnDisable()
    {
        M_Logic.OnGameStart -= InitTimer;
    }

    private void InitTimer(Config arg0)
    {
        _currentConfig = arg0;
        
    }

    void Open()
    {
        Timer = _currentConfig.Duration;
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
