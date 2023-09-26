using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace MoonActive.Scripts.Managers
{
    public class XMLManager
    {
        private static readonly string XMLFilePath = Path.Combine(Application.dataPath , "Resources", "Data", "XML" ,"GameData.xml");

        public static void SaveData<T>(T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(XMLFilePath, FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        public static T LoadData<T>()
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
    }
}