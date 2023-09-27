using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class M_Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;


    RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _rectTransform.anchoredPosition = Vector2.up * 100;
    }


    void Init()
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
