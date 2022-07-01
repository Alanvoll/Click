using System;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public static event Action OnGameStart; 
    public static event Action OnGameEnd;
    public event Action<int, int> OnEnemyCountChanged; 

    [SerializeField] private int _maxEnemyCount;
    private int _currentEnemyCount;

    private void Awake()
    {
        Enemy.OnSpawn += OnEnemySpawn;
        Enemy.OnDeath += OnEnemyDeath;
        GameStart();
    }

    private void OnEnemySpawn(Enemy enemy)
    {
        _currentEnemyCount++;
        OnEnemyCountChanged?.Invoke(_currentEnemyCount,_maxEnemyCount);
        if (_currentEnemyCount == _maxEnemyCount)
            GameEnd();
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        _currentEnemyCount--;
        OnEnemyCountChanged?.Invoke(_currentEnemyCount,_maxEnemyCount);
    }

    public void GameStart()
    {
        _currentEnemyCount = 0;
        OnEnemyCountChanged?.Invoke(_currentEnemyCount,_maxEnemyCount);
        OnGameStart?.Invoke();
    }
    
    public void GameEnd()
    {
        OnGameEnd?.Invoke();
    }
}