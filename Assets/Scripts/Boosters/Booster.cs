using System;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static event Action<Booster> OnSpawn;
    public static event Action<Booster> OnRelease; 
    
    [SerializeField] private FillBar _timeBar;
    [SerializeField] private float _maxLifeTime;
    private float _currentLifeTime;
    
    public float MAXLifeTime => _maxLifeTime;

    public float CurrentLifeTime => _currentLifeTime;
    
    private void Awake()
    {
        OnSpawn?.Invoke(this);
    }

    private void OnDestroy()
    {
        OnRelease?.Invoke(this);
    }
    
    private void Start()
    {
        _currentLifeTime = MAXLifeTime;
        OnStart();
    }

    private void Update()
    {
        _currentLifeTime = CurrentLifeTime - Time.deltaTime;
        if (CurrentLifeTime <= 0)
            Destroy(gameObject);
        
        _timeBar.SetAmount(CurrentLifeTime / MAXLifeTime);
    }
    
    private void OnMouseDown()
    {
        ApplyEffect();
        Destroy(gameObject);
    }
    
    protected virtual void OnStart(){}
    
    protected virtual void ApplyEffect(){}
}