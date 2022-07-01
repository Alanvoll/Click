using UnityEngine;


public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private FillBar _healthBar;

    private void Awake()
    {
        _enemy.OnHealthChanged += OnEnemyHealthChanged;
    }

    private void OnEnemyHealthChanged()
    {
        _healthBar.SetAmount(_enemy.Health / _enemy.MAXHealth);
    }
}