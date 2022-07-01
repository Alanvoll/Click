using System.Collections.Generic;

public interface IRecordManager : IService
{
    public void AddRecord(Record record);
    
    public IEnumerable<Record> GetRecords();
}