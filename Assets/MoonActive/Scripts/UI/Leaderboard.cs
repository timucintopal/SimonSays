using System.Collections.Generic;
using MoonActive.Scripts.Class;
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

        private float SlotTopMargin => leaderboardData.slotTopMargin;
        private float SlotMargin => leaderboardData.slotMargin;

        float _slotHeight;

        private void Start()
        {
            _slotHeight = slotPrefab.GetComponent<RectTransform>().sizeDelta.y;
        }

        void Init()
        {
            var slotDataStatus = PlayerPrefsData.SlotDataStatus();
            
            for (int i = 0; i < leaderboardData.slotAmount; i++)
            {
                var slot = Instantiate(slotPrefab, slotParent).GetComponent<LeaderboardSlot>();
                
                _slots.Add(slot);

                var targetPos = Vector2.down * ((_slotHeight * i)) + (Vector2.down * SlotMargin * i) +
                                (Vector2.down * ((_slotHeight / 2) + SlotTopMargin));

                if(slotDataStatus)
                {
                    var data = PlayerPrefsData.GetSlotData(i);
                    slot.Init(data.playerName, data.playerScore, targetPos, data.isPlayer);
                }
                else
                {
                    if(i+1 != leaderboardData.slotAmount)
                        slot.Init(leaderboardData.userNames[i], 0 , targetPos);
                    else
                    {
                        slot.Init(PlayerPrefsData.GetPlayerName(), 0, targetPos, true);
                        _playerSlot = slot;
                    }
                }
            }
        }

        // [ContextMenu("Open")]
        void Open()
        {
            if (_slots.Count == 0)
                Init();

            foreach (var slot in _slots)
            {
                
            }
        }

        void SaveSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                PlayerPrefsData.SetSlotData(slot.);
            }
        }
        
    }
}
