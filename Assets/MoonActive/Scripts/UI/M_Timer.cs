using DG.Tweening;
using MoonActive.Scripts;
using TMPro;
using UnityEngine;

public class M_Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    RectTransform _rectTransform;


    Config _currentConfig;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = Vector2.up * 100;
    }

    void InitTimer()
    {
        
    }

    void Open()
    {
        _rectTransform.DOAnchorPosY(-200, .5f).SetEase(Ease.OutExpo);
    }
    
    void Close()
    {
        
    }
}
