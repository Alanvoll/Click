using System.Collections.Generic;

public interface IEnemySpawner : IService
{
    public IEnumerable<Enemy> GetEnemies();
    public void FreezeSpawn(float time);
}