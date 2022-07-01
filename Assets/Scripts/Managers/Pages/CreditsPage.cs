using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public sealed class CreditsPage : BasePage
{
    [SerializeField] private GameObject _creditsListItemPrefab;
    [SerializeField] private CreditsSettings _creditsSettings;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Button _backButton;
    private IPageManager _pageManager;

    protected override void OnAwake()
    {
        _pageManager = ServiceProvider.GetService<IPageManager>();
        _backButton.onClick.AddListener(OnBackButtonClick);
        foreach (var credit in _creditsSettings.Credits)
        {
            var instance = Instantiate(_creditsListItemPrefab, _scrollRect.content.transform);
            var creditListItem = instance.GetComponent<CreditsListItem>();
            creditListItem.SetData(credit);
        }
    }

    private void OnBackButtonClick()
    {
        _pageManager.OpenPage<MainPage>();
    }
}