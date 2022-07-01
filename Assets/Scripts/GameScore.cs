using System;
using UnityEngine;
using UnityEngine.UI;


public class GameScore : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private LifeTimeScoreModifier _scoreModifier;
    private IRecordManager _recordManager;
    private int _score;
    private int _defaultScoreValue = 10;

    private void Awake()
    {
        _recordManager = ServiceProvider.GetService<IRecordManager>();
        GameController.OnGameStart += OnGameStart;
        GameController.OnGameEnd += OnGameEnd;
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        var addScoreValue = _defaultScoreValue;

        if (enemy.LifeTime < _scoreModifier.BestLifeTime)
            addScoreValue = (int) (addScoreValue * _scoreModifier.BestModifier);
        else if (enemy.LifeTime < _scoreModifier.GoodLifeTime)
            addScoreValue = (int) (addScoreValue * _scoreModifier.GoodModifier);
        else if (enemy.LifeTime < _scoreModifier.NormalLifeTime)
            addScoreValue = (int) (addScoreValue * _scoreModifier.NormalModifier);
        else
            addScoreValue = (int) (addScoreValue * _scoreModifier.BadModifier);

        _score += addScoreValue;
        _scoreText.text = $"Score: {_score}";
    }

    private void OnGameStart()
    {
        Enemy.OnDeath += OnEnemyDeath;
        _scoreText.text = $"Score: {_score}";
    }

    private void OnGameEnd()
    {
        Enemy.OnDeath -= OnEnemyDeath;
        var date = DateTime.Now;
        var record = new Record(_score, date);
        _recordManager.AddRecord(record);
        _score = 0;
    }

    [Serializable]
    private class LifeTimeScoreModifier
    {
        [SerializeField] private float _bestLifeTime;
        [SerializeField] private float _goodLifeTime;
        [SerializeField] private float _normalLifeTime;

        [SerializeField] private float _bestModifier;
        [SerializeField] private float _goodModifier;
        [SerializeField] private float _normalModifier;
        [SerializeField] private float _badModifier;


        public float BestLifeTime => _bestLifeTime;

        public float GoodLifeTime => _goodLifeTime;

        public float NormalLifeTime => _normalLifeTime;

        public float BestModifier => _bestModifier;

        public float GoodModifier => _goodModifier;

        public float NormalModifier => _normalModifier;

        public float BadModifier => _badModifier;
    }
}