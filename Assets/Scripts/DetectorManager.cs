using UnityEngine;

public class DetectorManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyDetectorPrefab;
    [SerializeField] private GameObject _boosterDetectorPrefab;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        Enemy.OnSpawn += OnEnemySpawn;
        Booster.OnSpawn += OnBoosterSpawn;
    }

    private void OnBoosterSpawn(Booster booster)
    {
        var instance = Instantiate(_boosterDetectorPrefab, _canvas.transform);
        var boosterDetector = instance.GetComponent<BoosterDetector>();
        boosterDetector.SetTarget(booster);
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        var instance = Instantiate(_enemyDetectorPrefab, _canvas.transform);
        var enemyDetector = instance.GetComponent<EnemyDetector>();
        enemyDetector.SetTarget(enemy);
    }
}