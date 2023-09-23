using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class M_PlayerPrefs : Singleton<M_PlayerPrefs>
    {
        /// <summary>
        /// Returns true if its players first entry
        /// </summary>
        /// <returns></returns>
        public static bool CheckFirstEntry()
        {
            return PlayerPrefs.GetInt("IsFirstEntry", 0) == 0;
        }

        public void SetName(string name)
        {
            
        }
    }
}
