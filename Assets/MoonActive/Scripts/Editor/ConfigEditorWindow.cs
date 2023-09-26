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
                _configs.Add(new Config());

            if (GUILayout.Button("SAVE"))
                SaveXML();
            
            EditorGUILayout.EndScrollView();
        }

        void Read()
        {
            _gameConfigs = XMLManager.LoadData<GameConfigs>();
            _configs = _gameConfigs.List;
        }

        void SaveXML()
        {
            XMLManager.SaveData(_gameConfigs);
        }
    }
}