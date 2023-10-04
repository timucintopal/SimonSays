using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public static class DataManager
    {
        static readonly string XMLFilePath = Path.Combine(Application.dataPath , "Resources", "Data", "XML" ,"GameData.xml");
        static readonly string JsonFilePath = Path.Combine(Application.dataPath, "Resources", "Data", "JSON" ,"GameData.json");

        public static void SaveDataXML<T>(T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(XMLFilePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        public static T LoadDataXML<T>()
        {
            if (File.Exists(XMLFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(XMLFilePath, FileMode.Open))
                    return (T)serializer.Deserialize(stream);
            }
            
            Debug.LogError("XML file not found.");
            return default(T);
        }
        
        public static void SaveDataJson<T>(T data)
        {
            Debug.Log("Save! " + Application.persistentDataPath);
            string levelTypeToJson = JsonUtility.ToJson(data);
            
            File.WriteAllText(JsonFilePath, levelTypeToJson);
        }
        
        public static T LoadDataJson<T>()
        {
            var jsonFile = Resources.Load<TextAsset>(Path.Combine("Data", "JSON" ,"GameData"));
            if (jsonFile)
            {
                string jsonTextFile = File.ReadAllText(JsonFilePath);
                return (T)JsonUtility.FromJson<T>(jsonTextFile);
            }
            Debug.LogError("FILE NULL");
            return default;
        }
    }
}