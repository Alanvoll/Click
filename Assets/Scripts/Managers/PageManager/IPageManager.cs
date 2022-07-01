public interface IPageManager : IService
{
    public void OpenPage<T>() where T : BasePage;
}