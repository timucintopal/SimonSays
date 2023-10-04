using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoonActive.Scripts.Class;
using MoonActive.Scripts.ScriptableObject;
using UnityEngine;

namespace MoonActive.Scripts.UI
{
    public class Leaderboard : MonoBehaviour
    {
        public LeaderboardSO leaderboardData;
        public GameObject slotPrefab;

        [SerializeField] private List<LeaderboardSlot> _slots = new List<LeaderboardSlot>();
        [SerializeField] private List<SlotData> _slotDatas = new List<SlotData>();
        [SerializeField] private LeaderboardSlot _playerSlot;
        [SerializeField] private Transform slotParent;

        private float SlotTopMargin => leaderboardData.slotTopMargin;
        private float SlotMargin => leaderboardData.slotMargin;

        float _slotHeight;

        private WaitForSeconds _waitSlotOpenDelayBtwSlots;
        private WaitForSeconds _waitSlotOpen;
        private WaitForSeconds _waitSlotMove;
        private WaitForSeconds _waitPlayerScoreInc;

        private int _playerScoreIncrement;


        private bool _isFinished = false;

        private void Awake()
        {
            _waitSlotOpenDelayBtwSlots = new WaitForSeconds(leaderboardData.slotOpenDelayBetweenSlots);
            _waitSlotMove = new WaitForSeconds(leaderboardData.slotMoveDuration);
            _waitPlayerScoreInc = new WaitForSeconds(leaderboardData.playerScoreIncDuration);
            _waitSlotOpen = new WaitForSeconds(leaderboardData.slotOpenDuration);
        }

        private void OnEnable()
        {
            M_Score.GameEndScore += GetScore;
        }
        
        private void OnDisable()
        {
            M_Score.GameEndScore -= GetScore;
        }

        private void GetScore(int arg0)
        {
            _playerScoreIncrement = arg0;
        }

        private void Start()
        {
            _slotHeight = slotPrefab.GetComponent<RectTransform>().sizeDelta.y;
            // Open();
        }

        void Init()
        {
            var slotDataStatus = PlayerPrefsData.SlotDataStatus();
            _slotDatas = new List<SlotData>();
            
            for (int i = 0; i < leaderboardData.slotAmount; i++)
            {
                var slot = Instantiate(slotPrefab, slotParent).GetComponent<LeaderboardSlot>();
                
                _slots.Add(slot);

                var targetPos = GetSlotPosition(i);

                SlotData slotData = new SlotData();

                if(slotDataStatus)
                {
                    slotData = PlayerPrefsData.GetSlotData(i);
                    slot.SlotData = slotData; 
                    slot.Init(slot.SlotData, targetPos);

                    if (slotData.isPlayer)
                        _playerSlot = slot;
                }
                else
                {
                    if(i+1 != leaderboardData.slotAmount)
                    {
                        slotData = new SlotData()
                        {
                            name = leaderboardData.userNames[i],
                            score = 0,
                            isPlayer = false
                        };
                        slot.Init(slotData, targetPos);
                    }
                    else
                    {
                        slotData = new SlotData()
                        {
                            name = PlayerPrefsData.GetPlayerName(),
                            score = 0,
                            isPlayer = true
                        };
                        slot.Init(slotData, targetPos);
                        _playerSlot = slot;
                    }
                }
                _slotDatas.Add(slotData);
            }
        }
        
        [ContextMenu("Open")]
        void Open()
        {
            if (_slots.Count == 0)
                Init();

            var playerNewScore = _playerSlot.SlotData.score + 5;// _playerScoreIncrement;

            foreach (var slotData in _slotDatas)
            {
                if(slotData != _playerSlot.SlotData)
                    slotData.score = Random.Range(-3, 3) + playerNewScore;
            }

            OrderSlots();
            SaveSlots();
            StartCoroutine(OpenSeq());
        }

        IEnumerator OpenSeq()
        {
            _isFinished = false;
            
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
        
                slot.Init(_slotDatas[i], GetSlotPosition(i));
                slot.Open(leaderboardData.slotOpenDuration);
                
                yield return _waitSlotOpenDelayBtwSlots;
            }
            
            yield return _waitSlotOpen;
        
            // _playerSlot.IncrementPoint( _playerScoreIncrement, leaderboardData.playerScoreIncDuration);
            _playerSlot.IncrementPoint( 5, leaderboardData.playerScoreIncDuration);

            yield return _waitPlayerScoreInc;
            
            OrderSlots();

            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                slot.MoveTo(GetSlotPosition(_slotDatas.IndexOf(slot.SlotData)), leaderboardData.slotMoveDuration);
            }

            yield return _waitSlotMove;
        }

        [ContextMenu("Close")]
        public void Close()
        {
            StartCoroutine(CloseSeq());
        }

        IEnumerator CloseSeq()
        {
            foreach (var slot in _slots)
            {
                slot.Close();
                yield return _waitSlotOpenDelayBtwSlots;
            }
        }

        void OrderSlots()
        {
            _slotDatas = _slotDatas.OrderByDescending(x => x.score).ToList();
        }

        void SaveSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                PlayerPrefsData.SetSlotData(slot.SlotData,i);
            }
        }

        Vector2 GetSlotPosition(int index)
        {
            return Vector2.down * ((_slotHeight * index)) + (Vector2.down * (SlotMargin * index)) +
                (Vector2.down * ((_slotHeight / 2) + SlotTopMargin));
        }
        
    }
}
