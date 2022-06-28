using Leopotam.EcsLite;

namespace Game.Ui
{
    public class ScreenManagerInitSystem : IEcsInitSystem
    {
        private readonly IScreenManager _screenManager;

        public ScreenManagerInitSystem(IScreenManager screenManager) => _screenManager = screenManager;

        public void Init(EcsSystems systems) => _screenManager.Init();
    }
}