using System.Linq;

public class DeathBooster : Booster
{
    private IEnemySpawner _enemySpawner;

    protected override void OnStart()
    {
        _enemySpawner = ServiceProvider.GetService<IEnemySpawner>();
    }

    protected override void ApplyEffect()
    {
        var enemies = _enemySpawner.GetEnemies();
        while (enemies.Count() > 0)
        {
            enemies.First().TakeDamage(1000);
        }
    }
}