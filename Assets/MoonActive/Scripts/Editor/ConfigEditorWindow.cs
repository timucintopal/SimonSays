using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MoonActive.Scripts.Editor
{
    public class ConfigEditorWindow : EditorWindow
    {
        // Define a dictionary to store configuration values.
        private Dictionary<string, object> configData = new Dictionary<string, object>();

        private Difficulties _difficulty;
        private int _intValue;
        private string _stringValue;
        private float _floatValue;
        private bool _boolValue;

        [MenuItem("Custom/Open Config Editor")]
        public static void ShowWindow()
        {
            GetWindow<ConfigEditorWindow>("Config Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Configuration Editor", EditorStyles.boldLabel);

            if (GUILayout.Button("Load Configuration"))
            {
                // You can load your configuration data from a file or any other source here.
                // For simplicity, let's hardcode some sample values.
                configData.Clear();
                
                configData["IntValue"] = _intValue;
                configData["StringValue"] = _stringValue;
                configData["FloatValue"] = _floatValue;
                configData["BoolValue"] = _boolValue;
                configData["Difficulty"] = _difficulty.ToString(); 
            }

            _difficulty = (Difficulties)EditorGUILayout.EnumFlagsField("Difficulty", _difficulty);
            _intValue = EditorGUILayout.IntField("Int Value", _intValue);
            _stringValue = EditorGUILayout.TextField("String Value", _stringValue);
            _floatValue = EditorGUILayout.FloatField("Float Value", _floatValue);
            _boolValue = EditorGUILayout.Toggle("Bool Value", _boolValue);
            

            if (GUILayout.Button("Print Configuration"))
            {
                // Print the loaded configuration data.
                foreach (var kvp in configData)
                {
                    EditorGUILayout.LabelField($"{kvp.Key}: {kvp.Value}");
                }
            }
        }
    }
}