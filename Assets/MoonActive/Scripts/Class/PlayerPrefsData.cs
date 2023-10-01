using UnityEngine;

namespace MoonActive.Scripts.Class
{
    public static class PlayerPrefsData
    {

        public static bool SlotDataStatus()
        {
            return PlayerPrefs.GetString("SlotData_Name_" + 0, "") != "";
        }

        public static (string playerName, int playerScore, bool isPlayer) GetSlotData(int slotId)
        {
            var slotName = PlayerPrefs.GetString("SlotData_Name_" + slotId, "");
            var slotScore = PlayerPrefs.GetInt("SlotData_Score_" + slotId, 0);
            var playerStatus = PlayerPrefs.GetInt("SlotData_IsPlayer_" + slotId, 0);

            return (slotName, slotScore, playerStatus == 1);
        }

        public static void SetSlotData(string name, int score, int slotId)
        {
            PlayerPrefs.SetString("SlotData_Name_" + slotId, name);
            PlayerPrefs.SetInt("SlotData_Score_" + slotId, score);
        }

        public static void SetPlayerName(string playerName)
        {
            PlayerPrefs.SetString("PlayerName", playerName);
        }

        public static string GetPlayerName()
        {
            return PlayerPrefs.GetString("PlayerName", "");
        }

        public static bool GetPlayerNameStatus()
        {
            return PlayerPrefs.GetString("PlayerName", "") != "";
        }
    }
}
