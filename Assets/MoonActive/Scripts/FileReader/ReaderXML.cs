using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace MoonActive.Scripts.FileReader
{
    public class ReaderXML : Singleton<ReaderXML>
    {
        private static readonly string filePath = Path.Combine("Data", "XML" ,"GameData");
        TextAsset xmlFile;

        XmlDocument xmlDoc = new XmlDocument();

        [SerializeField] GameConfigs _gameConfigs = new GameConfigs();

        private void Start()
        {
            Read();
        }

        void Read()
        {
            xmlFile = Resources.Load<TextAsset>(filePath);
            if(xmlFile)
            {
                xmlDoc.LoadXml(xmlFile.text);
                XmlNodeList configNodes = xmlDoc.SelectNodes("/gameData/difficulty");

                _gameConfigs.List = new List<Config>();

                if (configNodes == null) return;
                foreach (XmlNode configNode in configNodes)
                {
                    if (configNode.Attributes != null)
                    {
                        string level = configNode.SelectSingleNode("Name")?.InnerText;
                        int buttons = int.Parse(configNode.SelectSingleNode("ButtonAmount")?.InnerText!);
                        int pointsPerStep = int.Parse(configNode.SelectSingleNode("PointsPerStep")?.InnerText!);
                        int gameTime = int.Parse(configNode.SelectSingleNode("GameTime")?.InnerText!);
                        bool repeatMode = bool.Parse(configNode.SelectSingleNode("RepeatMode")?.InnerText!);
                        float bonusGameSpeed = float.Parse(configNode.SelectSingleNode("GameSpeed")?.InnerText!);

                        Config config = new Config()
                        {
                            DifficultyName = level,
                            ButtonAmount = buttons,
                            PointPerStep = pointsPerStep,
                            Duration = gameTime,
                            IsRepeating = repeatMode,
                            SpeedMultiplier = bonusGameSpeed
                        };

                        _gameConfigs.List.Add(config);

                    }
                }
                // Save();
            }
        }

        
        void Save()
        {
            Debug.Log("Save! " + Application.persistentDataPath);
            string levelTypeToJson = JsonUtility.ToJson(_gameConfigs);
            
            File.WriteAllText(Path.Combine(Application.dataPath, "Resources/Data/JSON/GameData.json"), levelTypeToJson);
        }

        public Config Load()
        {
            return null;
        }

        public void Save(Config config)
        {
        
        }
    }
}
