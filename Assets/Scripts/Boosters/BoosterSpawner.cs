using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _boosterPrefab;
        [SerializeField] private float _spawnInterval;
        private float _lastSpawnTime;

        private void OnEnable()
        {
            _lastSpawnTime = Time.time;
        }

        private void Update()
        {
            if (_lastSpawnTime + _spawnInterval >= Time.time)
                return;

            NavMesh.SamplePosition(Random.insideUnitSphere * 18 + Vector3.zero, out var navMeshHit, 18, NavMesh.AllAreas);
            var instance = Instantiate(_boosterPrefab, transform);
            instance.transform.position = navMeshHit.position;

            _lastSpawnTime = Time.time;
        }
    }
