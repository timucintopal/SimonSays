using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderboardSO", menuName = "Data/Leaderboard")]
public class LeaderboardSO : ScriptableObject
{
    public int slotAmount;
    public float slotMargin;
    public float slotTopMargin;

    [SerializeField] public List<string> userNames;

}