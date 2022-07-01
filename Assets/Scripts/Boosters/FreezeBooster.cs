using UnityEngine;


public sealed class FreezeBooster : Booster
{
    [SerializeField] private float _freezeTime;

    private IEnemySpawner _enemySpawner;

    protected override void OnStart()
    {
        _enemySpawner = ServiceProvider.GetService<IEnemySpawner>();
    }

    protected override void ApplyEffect()
    {
        _enemySpawner.FreezeSpawn(_freezeTime);
    }
}