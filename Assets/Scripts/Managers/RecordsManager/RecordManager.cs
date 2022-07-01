using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour, IRecordManager
{
    [SerializeField] private int _maxRecordsCount;
    private readonly List<Record> _records = new List<Record>();
    private IDataSaver<Record> _recordsSaver;

    private void Awake()
    {
        ServiceProvider.AddService<IRecordManager>(this);
        _recordsSaver = new LocalJsonDataSaver<Record>();
        var loadRecords = _recordsSaver.Load();
        if (loadRecords != null)
            _records.AddRange(loadRecords);
        
        DontDestroyOnLoad(gameObject);
    }

    public void AddRecord(Record record)
    {
        _records.Add(record);
        _records.Sort((recordA, recordB) => recordB.Score - recordA.Score);
        if (_records.Count > _maxRecordsCount)
            _records.RemoveAt(_records.Count - 1);
        
        _recordsSaver.Save(_records);
    }

    public IEnumerable<Record> GetRecords()
    {
        return _records;
    }
}