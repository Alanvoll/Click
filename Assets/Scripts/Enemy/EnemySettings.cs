using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Settings", menuName = "GameSettings/EnemySettings", order = 0)]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private List<EnemyParam> _enemyParams;

    public EnemyParam GetParamByType(EnemyType type)
    {
        var param = _enemyParams.First(param => param.Type == type);
        return param;
    }
}


[Serializable]
public class EnemyParam
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;

    public EnemyType Type => _type;

    public float Health => _health;

    public float Speed => _speed;
}