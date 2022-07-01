using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public sealed class RecordsPage : BasePage
{
    [SerializeField] private GameObject _recordsListItemPrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Button _backButton;
    [SerializeField] private int _maxRecordsCount;
    private readonly List<RecordsListItem> _recordsItems = new List<RecordsListItem>();
    private IPageManager _pageManager;
    private IRecordManager _recordManager;

    protected override void OnAwake()
    {
        _pageManager = ServiceProvider.GetService<IPageManager>();
        _backButton.onClick.AddListener(OnBackButtonClick);
        _recordManager = ServiceProvider.GetService<IRecordManager>();

        for (var i = 0; i < _maxRecordsCount; i++)
        {
            var instance = Instantiate(_recordsListItemPrefab, _scrollRect.content.transform);
            instance.SetActive(false);
            var recordsListItem = instance.GetComponent<RecordsListItem>();
            recordsListItem.SetNumber(i + 1);
            _recordsItems.Add(recordsListItem);
        }
    }

    protected override void OnBeforeOpen()
    {
        var number = 0;
        foreach (var record in _recordManager.GetRecords())
        {
            var recordItem = _recordsItems[number];
            if(!recordItem.gameObject.activeSelf)
                recordItem.gameObject.SetActive(true);
            
            number++;
            recordItem.SetData(record);
            
            if(number >= _maxRecordsCount)
                break;
        }
    }

    private void OnBackButtonClick()
    {
        _pageManager.OpenPage<MainPage>();
    }
}