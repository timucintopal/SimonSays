using MoonActive.Scripts.FileReader;
using UnityEngine;

public enum DataType
{
    XML,
    JSON
}

[CreateAssetMenu(fileName = "FileReaderSO", menuName = "Data/FileReader")]
public class FileReaderSO : ScriptableObject
{
    public DataType DataTypeTarget;

}
