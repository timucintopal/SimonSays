using TMPro;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
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
        public SlotData SlotData
        {
            get => _slotData;
            set
            {
                _slotData = value;
                
                normalBg.SetActive(!_slotData.isPlayer);
                playerBg.SetActive(_slotData.isPlayer);

                nameTxt.text = _slotData.name;
                scoreTxt.text = _slotData.score.ToString();
            }
        }

        private void Awake()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
        }

        public void Init( SlotData data, Vector2 targetPos)
        {
            SlotData = data;

            _rectTransform.anchoredPosition = targetPos;
        }
        
        
    }
}
