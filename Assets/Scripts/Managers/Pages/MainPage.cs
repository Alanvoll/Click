using UnityEngine;
using UnityEngine.UI;

public sealed class MainPage : BasePage
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _recordsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Toggle _soundToggle;

    private IAudioManager _audioManager;
    private IPageManager _pageManager;


    protected override void OnAwake()
    {
        _pageManager = ServiceProvider.GetService<IPageManager>();
        _audioManager = ServiceProvider.GetService<IAudioManager>();

        _newGameButton.onClick.AddListener(OnNewGameButtonClick);
        _creditsButton.onClick.AddListener(OnCreditsButtonClick);
        _recordsButton.onClick.AddListener(OnRecordsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        _soundToggle.isOn = !_audioManager.IsMute;
    }

    private void OnSoundToggleChanged(bool value)
    {
        _audioManager.SetMuteSound(!value);
    }


    private void OnNewGameButtonClick()
    {
        _pageManager.OpenPage<GamePage>();
    }

    private void OnCreditsButtonClick()
    {
        _pageManager.OpenPage<CreditsPage>();
    }

    private void OnRecordsButtonClick()
    {
        _pageManager.OpenPage<RecordsPage>();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}