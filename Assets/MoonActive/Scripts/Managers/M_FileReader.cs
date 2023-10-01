using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    public class M_FileReader : MonoBehaviour
    {
        [SerializeField] private FileReaderSO _fileReaderSO;

        GameConfigs _gameConfigs;

        public static UnityAction<GameConfigs> OnDataLoad;

        public GameConfigs GameConfigs
        {
            set
            {
                _gameConfigs = value;
                OnDataLoad?.Invoke(_gameConfigs);
            }
        }
            
        private void Start()
        {
            if (_fileReaderSO.DataTypeTarget == DataType.XML)
                GameConfigs = DataManager.LoadDataXML<GameConfigs>();
            else
                GameConfigs = DataManager.LoadDataJson<GameConfigs>();
        }
    }
}
