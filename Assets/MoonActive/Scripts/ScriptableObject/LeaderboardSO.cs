using System.Collections.Generic;
using UnityEngine;

namespace MoonActive.Scripts.ScriptableObject
{
    [CreateAssetMenu(fileName = "LeaderboardSO", menuName = "Data/Leaderboard")]
    public class LeaderboardSO : UnityEngine.ScriptableObject
    {
        public int slotAmount;
        public float slotMargin;
        public float slotTopMargin;
        public float slotOpenDelayBetweenSlots;
        public float slotMoveDuration;
        public float slotOpenDuration;
        public float playerScoreIncDuration;

        [SerializeField] public List<string> userNames;

    }
}