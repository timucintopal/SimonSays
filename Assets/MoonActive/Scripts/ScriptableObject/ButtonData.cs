using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoonActive.Scripts.ScriptableObject
{

    [Serializable]
    public class SpawnPos
    {
        public List<Vector3> spawnPositions;
    }
    
    
    [CreateAssetMenu(fileName = "ButtonDataSO", menuName = "Data/Button")]
    public class ButtonData : UnityEngine.ScriptableObject
    {
        public GameObject ButtonPrefab;

        [Space] 
        public float spawnDelay;
        public float colorDuration;
        public float xOffset;
        public float yOffset;

        [Space] 
        public List<SpawnPos> SpawnPosList = new List<SpawnPos>();

        [Space] 
        public List<Color> ButtonSelectColors = new List<Color>();

        public List<Vector3> GetSpawnList(int buttonAmount)
        {
            foreach (var spawnPos in SpawnPosList)
            {
                if (spawnPos.spawnPositions.Count == buttonAmount)
                    return spawnPos.spawnPositions;
            }

            return null;
        }
    }
}