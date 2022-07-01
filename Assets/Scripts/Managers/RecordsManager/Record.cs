using System;
using UnityEngine;

[Serializable]
public class Record : ISerializationCallbackReceiver
{
    [SerializeField] private string _serializationDate;
    [SerializeField] private int _score;
    private DateTime _date;


    public Record(int score, DateTime date)
    {
        _score = score;
        _date = date;
    }

    
    public int Score => _score;

    public DateTime Date => _date;

    
    public void OnBeforeSerialize()
    {
        _serializationDate = $"{_date}";
    }

    public void OnAfterDeserialize()
    {
        _date = DateTime.Parse(_serializationDate);
    }
}