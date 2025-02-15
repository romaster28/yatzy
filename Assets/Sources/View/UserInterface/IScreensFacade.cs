namespace Sources.View.UserInterface
{
    public interface IScreensFacade
    {
        void Open<T>(bool closeOthers) where T : IScreen;

        T Get<T>() where T : IScreen;
        
        void Open<T>() where T : IScreen;

        void Close<T>() where T : IScreen;

        void OpenAll();

        void CloseAll();
    }
}