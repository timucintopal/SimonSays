using TMPro;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
    public class LeaderboardSlot : MonoBehaviour
    {
        [SerializeField] GameObject normalBg;
        [SerializeField] GameObject playerBg;

        [Space, SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private TextMeshProUGUI scoreTxt;

        public void Init( string userName, int scoreAmount, Vector2 targetPos, bool isPlayer = false)
        {
            normalBg.SetActive(!isPlayer);
            playerBg.SetActive(isPlayer);
        }
    }
}
