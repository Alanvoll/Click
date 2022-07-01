using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class LocalJsonDataSaver<T> : IDataSaver<T>
{
    private const string SaveFileName = "SaveData";
    private readonly string _path;

    public LocalJsonDataSaver()
    {
        _path = Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    public void Save(IEnumerable<T> enumerable)
    {
        var wrapper = new Wrapper
        {
            Items = enumerable.ToArray()
        };
        var json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(_path, json);
    }

    IEnumerable<T> IDataSaver<T>.Load()
    {
        if (!File.Exists(_path))
            return null;

        var json = File.ReadAllText(_path);
        var wrapper = JsonUtility.FromJson<Wrapper>(json);
        return wrapper.Items;
    }

    [Serializable]
    private class Wrapper
    {
        public T[] Items;
    }
}