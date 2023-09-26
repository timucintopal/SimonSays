using System.Collections.Generic;
using System.IO;
using System.Xml;
using MoonActive.Scripts;
using UnityEngine;

public interface IReadXml 
{
    private static readonly string filePath = Path.Combine("Data", "XML" ,"GameData");
    

    // [SerializeField] GameConfigs _gameConfigs = new GameConfigs();
    public void Read()
    {
        Debug.Log("HUHUUUU");
        TextAsset xmlFile;
        XmlDocument xmlDoc = new XmlDocument();
        XmlNodeList configNodes;
        GameConfigs _gameConfigs = new GameConfigs();
        
        xmlFile = Resources.Load<TextAsset>(filePath);
        if(xmlFile)
        {
            xmlDoc.LoadXml(xmlFile.text);
            configNodes = xmlDoc.SelectNodes("/gameData/difficulty");
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
        }
        
        
    }
}
