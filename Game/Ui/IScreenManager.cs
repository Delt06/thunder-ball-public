namespace Game.Ui
{
    public interface IScreenManager
    {
        void Init();
        TScreen Get<TScreen>() where TScreen : IScreen;
    }
}