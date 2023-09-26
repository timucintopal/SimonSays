using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

namespace MoonActive.Scripts.Editor
{
    public enum Pages
    {
        Main,
        Load,
        Save
    }
    public class ConfigEditorWindow : EditorWindow
    {
        // Define a dictionary to store configuration values.
        // private Dictionary<string, object> configData = new Dictionary<string, object>();

        List<Config> _configs = new List<Config>();

        Pages _pages = Pages.Main;

        private string _difficulty;
        private int _intValue;
        private string _stringValue;
        private float _floatValue;
        private bool _boolValue;

        bool isSave = false;

        [MenuItem("Custom/Open Config Editor")]
        public static void ShowWindow()
        {
            GetWindow<ConfigEditorWindow>("Config Editor");
        }
        
        private Vector2 scrollPosition;

        private void OnGUI()
        {
            // if(_pages == Pages.Main)
            // {
            //     if (GUILayout.Button("Load Configuration"))
            //     {
            //         // configData.Clear();
            //         //
            //         // configData["IntValue"] = _intValue;
            //         // configData["StringValue"] = _stringValue;
            //         // configData["FloatValue"] = _floatValue;
            //         // configData["BoolValue"] = _boolValue;
            //         // configData["Difficulty"] = _difficulty.ToString();
            //     }
            //
            //     if (GUILayout.Button("READ MENU"))
            //     {
            //         Read();
            //         _pages = Pages.Save;
            //         // Print the loaded configuration data.
            //         // foreach (var kvp in configData)
            //         // {
            //         //     EditorGUILayout.LabelField($"{kvp.Key}: {kvp.Value}");
            //         // }
            //     }
            // }
            // else if(_pages == Pages.Save)
            // {
            
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                for (var i = 0; i < _configs.Count; i++)
                {
                    var config = _configs[i];
                    GUILayout.Label("Config " + (i + 1), EditorStyles.boldLabel);
                    config.DifficultyName = EditorGUILayout.TextField("Name_" + i, config.DifficultyName);
                    config.ButtonAmount = EditorGUILayout.IntField("ButtonAmount_" + i, config.ButtonAmount);
                    config.PointPerStep = EditorGUILayout.IntField("Point_" + i, config.PointPerStep);
                    config.Duration = EditorGUILayout.IntField("Duration_" + i, config.Duration);
                    config.IsRepeating = EditorGUILayout.Toggle("Repeating_" + i, config.IsRepeating);
                    config.SpeedMultiplier = EditorGUILayout.FloatField("SpeedMultiplier_" + i, config.SpeedMultiplier);

                    if (GUILayout.Button("DeleteConfig"))
                    {
                        _configs.RemoveAt(i);
                        i--;
                    }

                    EditorGUILayout.Space();

                }

                if (GUILayout.Button("READ DATA"))
                    Read();

                if (GUILayout.Button("Add Config"))
                {
                    Config newConfig = new Config(); 
                    _configs.Add(newConfig);
                }

                if (GUILayout.Button("SAVE"))
                    SaveXML();
                
                EditorGUILayout.EndScrollView();
            // }
        }
        
        private static readonly string filePath = Path.Combine("Data", "XML" ,"GameData");
        TextAsset xmlFile;

        XmlDocument xmlDoc = new XmlDocument();

        [SerializeField] GameConfigs _gameConfigs = new GameConfigs();
        
        void Read()
        {
            _configs.Clear();
            xmlFile = Resources.Load<TextAsset>(filePath);
            if(xmlFile)
            {
                xmlDoc.LoadXml(xmlFile.text);
                XmlNodeList difficultyNodes = xmlDoc.SelectNodes("/gameData/difficulty");

                if (difficultyNodes != null)
                    foreach (XmlNode difficultyNode in difficultyNodes)
                    {
                        if (difficultyNode.Attributes != null)
                        {
                            string level = difficultyNode.SelectSingleNode("Name")?.InnerText;
                            int buttons = int.Parse(difficultyNode.SelectSingleNode("ButtonAmount")?.InnerText);
                            int pointsPerStep = int.Parse(difficultyNode.SelectSingleNode("PointsPerStep")?.InnerText);
                            int gameTime = int.Parse(difficultyNode.SelectSingleNode("GameTime")?.InnerText);
                            bool repeatMode = bool.Parse(difficultyNode.SelectSingleNode("RepeatMode")?.InnerText);
                            float bonusGameSpeed = float.Parse(difficultyNode.SelectSingleNode("GameSpeed")?.InnerText);

                            Config config = new Config()
                            {
                                DifficultyName = level,
                                ButtonAmount = buttons,
                                PointPerStep = pointsPerStep,
                                Duration = gameTime,
                                IsRepeating = repeatMode,
                                SpeedMultiplier = bonusGameSpeed
                            };

                            _configs.Add(config);

                            // Now, you can use the extracted data as needed for your game.
                            // Debug.Log($"Level: {level}");
                            // Debug.Log($"Buttons: {buttons}");
                            // Debug.Log($"Points Per Step: {pointsPerStep}");
                            // Debug.Log($"Game Time: {gameTime}");
                            // Debug.Log($"Repeat Mode: {repeatMode}");
                            // Debug.Log($"Bonus Game Speed: {bonusGameSpeed}");
                        }
                    }
                // Save();
            }
        }

        void SaveXML()
        {
            _gameConfigs = new GameConfigs();
            _gameConfigs.List = new List<Config>(_configs);
            
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfigs));
            using (FileStream stream = new FileStream(Path.Combine(Application.dataPath,"Resources" ,"Data", "XML", "GameData.xml"), FileMode.Create))
            {
                serializer.Serialize(stream, _gameConfigs);
            }
        }
    }
}