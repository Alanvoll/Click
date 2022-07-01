using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PageManager : MonoBehaviour, IPageManager
{
    [SerializeField] private List<GameObject> _pagePrefabs;

    private Dictionary<Type, BasePage> _pages = new Dictionary<Type, BasePage>();
    private BasePage _activePage;

    private void Awake()
    {
        ServiceProvider.AddService<IPageManager>(this);
        DontDestroyOnLoad(gameObject);
    }

    public void OpenPage<T>() where T : BasePage
    {
        if (!_pages.TryGetValue(typeof(T), out var openingPage))
            return;

        if (_activePage)
            _activePage.Close();
        
        _activePage = openingPage;
        _activePage.Open();
    }

    public void Initialize()
    {
        foreach (var page in _pagePrefabs.Select(pagePrefab =>
                Instantiate(pagePrefab, transform))
            .Select(instance => instance.GetComponent<BasePage>()))
        {
            _pages.Add(page.GetType(), page);
        }
    }
}