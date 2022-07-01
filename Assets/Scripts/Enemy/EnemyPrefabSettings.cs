using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Prefab Settings", menuName = "GameSettings/EnemyPrefabSettings", order = 0)]
    public class EnemyPrefabSettings : ScriptableObject
    {
        [SerializeField] private List<EnemyPrefab> _prefabs;

        public GameObject GetPrefabByType(EnemyType type)
        {
            return _prefabs.First(enemyPrefab => enemyPrefab.Type == type).Prefab;
        }

        [Serializable]
        private sealed class EnemyPrefab
        {
            [SerializeField] private EnemyType _type;
            [SerializeField] private GameObject _prefab;

            public EnemyType Type => _type;

            public GameObject Prefab => _prefab;
        }
    }
}