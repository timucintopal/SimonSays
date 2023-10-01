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

        private void Awake()
        {
            _rectTransform = transform.GetComponent<RectTransform>();
        }

        public void Init( string userName, int scoreAmount, Vector2 targetPos, bool isPlayer = false)
        {
            normalBg.SetActive(!isPlayer);
            playerBg.SetActive(isPlayer);

            nameTxt.text = userName;
            scoreTxt.text = scoreAmount.ToString();

            _rectTransform.anchoredPosition = targetPos;
        }
        
        
    }
}
