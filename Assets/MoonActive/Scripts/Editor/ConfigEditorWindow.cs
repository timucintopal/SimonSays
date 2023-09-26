using System.Collections.Generic;
using MoonActive.Scripts.Managers;
using UnityEditor;
using UnityEngine;

namespace MoonActive.Scripts.Editor
{
    public class ConfigEditorWindow : EditorWindow 
    {
        GameConfigs _gameConfigs = new GameConfigs();
        List<Config> _configs = new List<Config>();

        Vector2 scrollPosition;

        [MenuItem("Custom/Open Config Editor")]
        public static void ShowWindow()
        {
            GetWindow<ConfigEditorWindow>("Config Editor");
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            for (var i = 0; i < _configs.Count; i++)
            {
                var config = _configs[i];
                GUILayout.Label("Config " + (i + 1), EditorStyles.boldLabel);
                config.DifficultyName = EditorGUILayout.TextField("Name : ", config.DifficultyName);
                config.ButtonAmount = EditorGUILayout.IntField("ButtonAmount : ", config.ButtonAmount);
                config.PointPerStep = EditorGUILayout.IntField("Point : ", config.PointPerStep);
                config.Duration = EditorGUILayout.IntField("Duration : ", config.Duration);
                config.IsRepeating = EditorGUILayout.Toggle("Repeating : ", config.IsRepeating);
                config.SpeedMultiplier = EditorGUILayout.FloatField("SpeedMultiplier : ", config.SpeedMultiplier);

                if (GUILayout.Button("DeleteConfig"))
                {
                    _configs.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.Space();
            }
            
            if (GUILayout.Button("Add Config"))
                _configs.Add(new Config());

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Read Data From XML"))
                ReadXML();
            
            if (GUILayout.Button("Read Data From Json"))
                ReadJson();
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("SAVE To XML"))
                SaveXML();
            
            if (GUILayout.Button("SAVE To Json"))
                SaveJson();
            
            EditorGUILayout.EndScrollView();
        }

        void ReadXML()
        {
            _gameConfigs = DataManager.LoadDataXML<GameConfigs>();
            _configs = _gameConfigs.List;
        }
        
        void ReadJson()
        {
            _gameConfigs = DataManager.LoadDataJson<GameConfigs>();
            _configs = _gameConfigs.List;
        }

        void SaveXML()
        {
            DataManager.SaveDataXML(_gameConfigs);
        }
        
        void SaveJson()
        {
            DataManager.SaveDataJson(_gameConfigs);
        }
    }
}