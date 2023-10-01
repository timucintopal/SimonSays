using System.Collections.Generic;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
    public class Leaderboard : MonoBehaviour
    {
        public LeaderboardSO leaderboardData;
        public GameObject slotPrefab;

        private List<LeaderboardSlot> _slots = new List<LeaderboardSlot>();
        private LeaderboardSlot _playerSlot;
        [SerializeField] private Transform slotParent;

        float _slotHeight;

        private void Start()
        {
            _slotHeight = slotPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        void Init()
        {
            for (int i = 0; i < leaderboardData.slotAmount; i++)
            {
                var Instantiate(slotPrefab,)
            }
        }

        void Open()
        {
            if (_slots.Count == 0)
            {
                Init();
            }
            
        }
        
        
    }
}
