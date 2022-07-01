using UnityEngine;

public abstract class BasePage : MonoBehaviour
{
    #region Initialization

    public void Awake()
    {
        OnAwake();
        gameObject.SetActive(false);
    }

    protected virtual void OnAwake()
    {
    }

    public void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
    }

    #endregion

    #region Open

    public void Open()
    {
        OnBeforeOpen();
        gameObject.SetActive(true);
        OnAfterOpen();
    }

    protected virtual void OnBeforeOpen()
    {
    }

    protected virtual void OnAfterOpen()
    {
    }

    #endregion

    #region Close

    public void Close()
    {
        OnBeforeClose();
        gameObject.SetActive(false);
        OnAfterClose();
    }

    protected virtual void OnBeforeClose()
    {
    }

    protected virtual void OnAfterClose()
    {
    }

    #endregion
}