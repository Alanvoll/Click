using System.Collections.Generic;
using DefaultNamespace.Enemy;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private EnemyPrefabSettings _prefabSettings;
    [SerializeField] private int _enemyNeedKilledForIntervalModifier;
    [SerializeField] private float _intervalModifier;
    [SerializeField] private int _enemyNeedKilledForParamModifier;
    [SerializeField] private float _paramIncreaseFactor;
    [SerializeField] private float _defaultSpawnInterval;
    [SerializeField] private float _firstSpawnDelay;
    private float _paramModifier;
    private readonly List<Enemy> _enemies = new List<Enemy>();
    private float _spawnInterval;
    private float _lastSpawnTime;
    private IAudioManager _audioManager;
    private int _spawnCounter;

    private void Awake()
    {
        _audioManager = ServiceProvider.GetService<IAudioManager>();
        _lastSpawnTime = _firstSpawnDelay - _spawnInterval;
        ServiceProvider.AddService<IEnemySpawner>(this);
        Enemy.OnDeath += OnEnemyDeath;
        GameController.OnGameStart += OnGameStart;
        GameController.OnGameEnd += OnGameEnd;
    }

    private void Update()
    {
        if (_lastSpawnTime + _spawnInterval >= Time.time)
            return;

        NavMesh.SamplePosition(Random.insideUnitSphere * 20 + Vector3.zero, out var navMeshHit, 20, NavMesh.AllAreas);
        var enemyType = Random.Range(0, 3) switch
        {
            0 => EnemyType.Quickly,
            1 => EnemyType.Strong,
            _ => EnemyType.Normal
        };
        var prefab = _prefabSettings.GetPrefabByType(enemyType);
        var instance = Instantiate(prefab, transform);
        instance.transform.position = navMeshHit.position;
        var enemy = instance.GetComponent<Enemy>();
        enemy.SetModifier(_paramModifier);
        _enemies.Add(enemy);
        _spawnCounter++;
        if (_spawnCounter % _enemyNeedKilledForIntervalModifier == 0)
            _spawnInterval *= _intervalModifier;
        
        if (_spawnCounter % _enemyNeedKilledForParamModifier == 0)
            _paramModifier *= _paramIncreaseFactor;

        _audioManager.PlaySFX(AudioClipType.EnemySpawn);

        _lastSpawnTime = Time.time;
    }

    public IEnumerable<Enemy> GetEnemies()
    {
        return _enemies;
    }

    public void FreezeSpawn(float time)
    {
        _lastSpawnTime += time;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }

    private void OnGameStart()
    {
        _spawnInterval = _defaultSpawnInterval;
        _spawnCounter = 0;
        _paramModifier = 1;
    }

    private void OnGameEnd()
    {
        foreach (var enemy in _enemies)
            Destroy(enemy.gameObject);

        _enemies.Clear();
    }
}