using UnityEngine;
using UnityEngine.UI;

public sealed class GamePage : BasePage
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Text _enemyCounterText;

    private IPageManager _pageManager;

    protected override void OnAwake()
    {
        _pageManager = ServiceProvider.GetService<IPageManager>();
        _menuButton.onClick.AddListener(OnMenuButtonClick);
        _gameController.OnEnemyCountChanged += OnEnemyCountChanged;
        GameController.OnGameEnd += OnGameEnd;
    }

    private void OnEnemyCountChanged(int currentValue, int maxValue)
    {
        _enemyCounterText.text = $"Enemy: {currentValue} / {maxValue}";
    }

    protected override void OnAfterOpen()
    {
        _gameController.GameStart();
    }

    private void OnGameEnd()
    {
        _pageManager.OpenPage<MainPage>();
    }

    private void OnMenuButtonClick()
    {
        _gameController.GameEnd();
    }
}