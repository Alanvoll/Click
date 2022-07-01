using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private PageManager _pageManager;

    private void Start()
    {
        _pageManager.Initialize();
        _pageManager.OpenPage<MainPage>();
        Destroy(gameObject);
    }
}