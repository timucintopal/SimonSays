using UnityEngine;

public class M_FileReader : MonoBehaviour
{
    [SerializeField] private FileReaderSO _fileReaderSO;

    private void Start()
    {
        // if (_fileReaderSO.DataTypeTarget == DataType.XML)
        //     transform.AddComponent<ReaderXML>();
        // else
        //     transform.AddComponent<ReaderJSON>();
    }
    
    
}
