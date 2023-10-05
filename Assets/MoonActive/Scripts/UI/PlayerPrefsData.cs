using UnityEngine;

namespace MoonActive.Scripts.UI
{
    public static class PlayerPrefsData
    {

        public static bool SlotDataStatus()
        {
            return PlayerPrefs.GetString("SlotData_Name_" + 0, "") != "";
        }

        public static SlotData GetSlotData(int slotId)
        {
            SlotData slotData = new SlotData()
            {
                name = PlayerPrefs.GetString("SlotData_Name_" + slotId, ""),
                score = PlayerPrefs.GetInt("SlotData_Score_" + slotId, 0),
                isPlayer = PlayerPrefs.GetInt("SlotData_IsPlayer_" + slotId, 0) == 1,
            };

            return slotData;
        }

        public static void SetSlotData(SlotData data, int slotId)
        {
            PlayerPrefs.SetString("SlotData_Name_" + slotId, data.name);
            PlayerPrefs.SetInt("SlotData_Score_" + slotId, data.score);
            PlayerPrefs.SetInt("SlotData_IsPlayer_" + slotId, data.isPlayer ? 1 : 0);
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
