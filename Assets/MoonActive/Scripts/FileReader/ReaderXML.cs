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
            xmlFile = Resources.Load<TextAsset>(filePath);
            if(xmlFile)
            {
                xmlDoc.LoadXml(xmlFile.text);
                XmlNodeList difficultyNodes = xmlDoc.SelectNodes("/gameData/difficulty");

                _gameConfigs.List = new List<Config>();
                
                foreach (XmlNode difficultyNode in difficultyNodes)
                {
                    if (difficultyNode.Attributes != null)
                    {
                        string level = difficultyNode.Attributes["level"].Value;
                        ;
                        int buttons = int.Parse(difficultyNode.SelectSingleNode("buttons")?.InnerText);
                        int pointsPerStep = int.Parse(difficultyNode.SelectSingleNode("pointsPerStep").InnerText);
                        int gameTime = int.Parse(difficultyNode.SelectSingleNode("gameTime").InnerText);
                        bool repeatMode = bool.Parse(difficultyNode.SelectSingleNode("repeatMode").InnerText);
                        float bonusGameSpeed = float.Parse(difficultyNode.SelectSingleNode("bonus/gameSpeed").InnerText);

                        Config config = new Config()
                        {
                            Difficulty = (Difficulties)System.Enum.Parse( typeof(Difficulties), level ),
                            ButtonAmount = buttons,
                            PointPerStep = pointsPerStep,
                            Duration = gameTime,
                            IsRepeating = repeatMode,
                            SpeedMultiplier = bonusGameSpeed
                        };
                        
                        _gameConfigs.List.Add(config);

                        // Now, you can use the extracted data as needed for your game.
                        // Debug.Log($"Level: {level}");
                        // Debug.Log($"Buttons: {buttons}");
                        // Debug.Log($"Points Per Step: {pointsPerStep}");
                        // Debug.Log($"Game Time: {gameTime}");
                        // Debug.Log($"Repeat Mode: {repeatMode}");
                        // Debug.Log($"Bonus Game Speed: {bonusGameSpeed}");
                    }
                }
                Save();
            }
            else
                Debug.Log("XML NULL");
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
