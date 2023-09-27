using MoonActive.Scripts;
using MoonActive.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class M_FileReader : MonoBehaviour
{
    [SerializeField] private FileReaderSO _fileReaderSO;

    GameConfigs _gameConfigs;

    public static UnityAction<GameConfigs> OnDataLoad;

    public GameConfigs GameConfigs
    {
        get => _gameConfigs;
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
            GameConfigs = DataManager.LoadDataXML<GameConfigs>();
    }
}
