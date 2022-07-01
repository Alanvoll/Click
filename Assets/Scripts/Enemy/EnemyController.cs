using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _moveAnimationName;

    [SerializeField] private float _changeWayInterval;
    [SerializeField] private float _minChangeWayInterval;
    [SerializeField] private float _maxChangeWayInterval;
    private float _lastChangeWayTime;
    private IAudioManager _audioManager;
    private NavMeshAgent _agent;
    private Vector3 _nextPosition;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioManager = ServiceProvider.GetService<IAudioManager>();
    }

    private void Update()
    {
        if (_agent.isStopped || _lastChangeWayTime + _changeWayInterval <= Time.time)
            MoveToNextPosition();
    }

    private void MoveToNextPosition()
    {
        NavMesh.SamplePosition(Random.insideUnitSphere * 20 + Vector3.zero, out var navMeshHit, 20,
            NavMesh.AllAreas);
        _nextPosition = navMeshHit.position;
        _enemy.MoveToPoint(_nextPosition);
        _animator.Play(_moveAnimationName);
        _changeWayInterval = Random.Range(_minChangeWayInterval, _maxChangeWayInterval);
        _lastChangeWayTime = Time.time;
    }

    private void OnMouseDown()
    {
        _audioManager.PlaySFX(AudioClipType.EnemyHit);
        _enemy.TakeDamage(1);
    }
}