using System.IO;
using UnityEngine;

namespace MoonActive.Scripts.FileReader
{
    public class ReaderJSON : Singleton<ReaderJSON>
    {
        static readonly string filePath = Path.Combine("Data", "JSON" ,"GameData");
        TextAsset jsonFile;
        
        [SerializeField] GameConfigs gameConfigs = new GameConfigs();

        private void Start()
        {
            jsonFile = Resources.Load<TextAsset>(filePath);
            if (jsonFile)
            {
                Debug.Log("FILE EXIST");
                string jsonTextFile = File.ReadAllText(filePath);
                gameConfigs = JsonUtility.FromJson<GameConfigs>(jsonTextFile);
            }
            else
            {
                Debug.Log("FILE NULL");
            }
        }

        public GameConfigs Load()
        {
            return gameConfigs;
        }

        public void Save(GameConfigs gameConfig)
        {
            
        }
    }
}
