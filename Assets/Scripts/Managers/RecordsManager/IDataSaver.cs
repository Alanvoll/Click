using System.Collections.Generic;

public interface IDataSaver<T>
{
    public void Save(IEnumerable<T> records);
    public IEnumerable<T> Load();
}