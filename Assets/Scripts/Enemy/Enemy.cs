using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnSpawn;
    public static event Action<Enemy> OnDeath;
    public event Action OnHealthChanged;

    [SerializeField] private EnemyType _type;
    [SerializeField] private EnemySettings _enemySettings;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    private NavMeshAgent _agent;
    private float _spawnTime;

    public float LifeTime => Time.time - _spawnTime;

    public float MAXHealth => _maxHealth;

    public float Health => _health;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        Spawn();
    }

    private void Spawn()
    {
        var param = _enemySettings.GetParamByType(_type);
        _agent.speed = param.Speed;
        _maxHealth = param.Health;
        _health = MAXHealth;

        _spawnTime = Time.time;
        OnSpawn?.Invoke(this);
    }

    public void MoveToPoint(Vector3 endPoint)
    {
        _agent.SetDestination(endPoint);
    }

    public void TakeDamage(int value)
    {
        _health = Health - value;
        OnHealthChanged?.Invoke();
        if (Health <= 0)
            Death();
    }

    private void Death()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void SetModifier(float paramModifier)
    {
        _agent.speed *= paramModifier;
        _maxHealth *= paramModifier;
        _health = MAXHealth;

        OnHealthChanged?.Invoke();
    }
}