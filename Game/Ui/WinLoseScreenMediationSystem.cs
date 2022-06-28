using Cysharp.Threading.Tasks;
using Game.Levels;
using Game.Player;
using Game.Waves;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ui
{
    public class WinLoseScreenMediationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnPlayerDied>> _deathFilter = default;
        private readonly EcsFilterInject<Inc<OnFinishedWaves>> _finishFilter = default;
        private readonly LevelLoader _levelLoader;
        private readonly IScreenManager _screenManager;

        public WinLoseScreenMediationSystem(IScreenManager screenManager, LevelLoader levelLoader)
        {
            _screenManager = screenManager;
            _levelLoader = levelLoader;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var _ in _deathFilter)
            {
                ShowLoseScreenAsync().Forget();
                break;
            }

            foreach (var _ in _finishFilter)
            {
                ShowWinScreenAsync().Forget();
                break;
            }
        }

        private async UniTask ShowLoseScreenAsync()
        {
            CloseGameplayScreen();
            var screen = _screenManager.Get<LoseScreen>();
            await screen.OpenAsync();
            _levelLoader.Reload();
        }

        private async UniTask ShowWinScreenAsync()
        {
            CloseGameplayScreen();
            var screen = _screenManager.Get<WinScreen>();
            await screen.OpenAsync();
            _levelLoader.Reload();
        }

        private void CloseGameplayScreen()
        {
            _screenManager.Get<GameplayScreen>().Close();
        }
    }
}