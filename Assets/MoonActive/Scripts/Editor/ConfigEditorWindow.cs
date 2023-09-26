using System.Collections.Generic;
using System.IO;
using System.Xml;
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
        private List<string> stringList = new List<string>(){"abi", "abe", "hoooo"};

        private void OnGUI()
        {
            // GUILayout.Label("Configuration Editor", EditorStyles.boldLabel);
            //
            // GUILayout.Label("List of Strings", EditorStyles.boldLabel);
            //
            //
            // // Scroll view to handle a long list of strings
            // scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            //
            // for (int i = 0; i < stringList.Count; i++)
            // {
            //     stringList[i] = EditorGUILayout.TextField("String " + i, stringList[i]);
            //
            //     // You can add buttons to delete or perform actions on each string if needed
            //     if (GUILayout.Button("Delete"))
            //     {
            //         stringList.RemoveAt(i);
            //         i--;
            //     }
            // }
            //
            // EditorGUILayout.EndScrollView();
            //
            // // Add a button to add new strings to the list
            // if (GUILayout.Button("Add String"))
            // {
            //     stringList.Add("");
            // }

            if(_pages == Pages.Main)
            {
                if (GUILayout.Button("Load Configuration"))
                {
                    // configData.Clear();
                    //
                    // configData["IntValue"] = _intValue;
                    // configData["StringValue"] = _stringValue;
                    // configData["FloatValue"] = _floatValue;
                    // configData["BoolValue"] = _boolValue;
                    // configData["Difficulty"] = _difficulty.ToString();
                }

                if (GUILayout.Button("READ MENU"))
                {
                    Read();
                    _pages = Pages.Save;
                    // Print the loaded configuration data.
                    // foreach (var kvp in configData)
                    // {
                    //     EditorGUILayout.LabelField($"{kvp.Key}: {kvp.Value}");
                    // }
                }
            }
            else if(_pages == Pages.Save)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                for (var i = 0; i < _configs.Count; i++)
                {
                    var config = _configs[i];
                    GUILayout.Label("Config " + (i + 1), EditorStyles.boldLabel);
                    EditorGUILayout.TextField("Name_" + i, config.DifficultyName);
                    EditorGUILayout.IntField("ButtonAmount_" + i, config.ButtonAmount);
                    EditorGUILayout.IntField("Point_" + i, config.PointPerStep);
                    EditorGUILayout.IntField("Duration_" + i, config.Duration);
                    EditorGUILayout.Toggle("Repeating_" + i, config.IsRepeating);
                    EditorGUILayout.FloatField("SpeedMultiplier_" + i, config.SpeedMultiplier);

                    if (GUILayout.Button("DeleteConfig"))
                    {
                        _configs.RemoveAt(i);
                        i--;
                    }

                    EditorGUILayout.Space();

                }

                if (GUILayout.Button("Add Config"))
                {
                    Config newConfig = new Config(); 
                    _configs.Add(new Config());
                }

                if (GUILayout.Button("SAVE CONFIGS MENU"))
                    _pages = Pages.Main;
                
                EditorGUILayout.EndScrollView();

            }
            
        }
        
        private static readonly string filePath = Path.Combine("Data", "XML" ,"GameData");
        TextAsset xmlFile;

        XmlDocument xmlDoc = new XmlDocument();

        [SerializeField] GameConfigs _gameConfigs = new GameConfigs();
        
        void Read()
        {
            xmlFile = Resources.Load<TextAsset>(filePath);
            if(xmlFile)
            {
                xmlDoc.LoadXml(xmlFile.text);
                XmlNodeList difficultyNodes = xmlDoc.SelectNodes("/gameData/difficulty");

                // _gameConfigs.List = new List<Config>();
                
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
    }
}