using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalSaveSystem<T> : ISaveSystem<T>
{
    private readonly string _path;

    public LocalSaveSystem(string fileName)
    {
#if UNITY_EDITOR
        _path = Application.dataPath + "\\Saves\\";
#else
        _path = Application.persistentDataPath + "\\Saves\\";
#endif

        if (!Directory.Exists(_path))
            Directory.CreateDirectory(_path);

        _path += fileName;
        if (!File.Exists(_path))
            Save(default);
    }

    public void Save(T data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(_path);

        formatter.Serialize(saveFile, data);

        saveFile.Close();
    }

    public T Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(_path, FileMode.Open);

        T data = (T)formatter.Deserialize(saveFile);

        saveFile.Close();

        return data;
    }
}
