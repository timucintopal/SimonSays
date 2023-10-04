using UnityEngine;
using UnityEngine.Events;

namespace MoonActive.Scripts.Managers
{
    public class FileReader : MonoBehaviour
    {
        [SerializeField] private FileReaderSO fileReaderSo;

        private GameConfigs _gameConfigs;

        public static UnityAction<GameConfigs> OnDataLoad;

            
        private void Start()
        {
            if (fileReaderSo.DataTypeTarget == DataType.XML)
                _gameConfigs = DataManager.LoadDataXML<GameConfigs>();
            else
                _gameConfigs = DataManager.LoadDataJson<GameConfigs>();
            
            OnDataLoad?.Invoke(_gameConfigs);
        }
    }
}
