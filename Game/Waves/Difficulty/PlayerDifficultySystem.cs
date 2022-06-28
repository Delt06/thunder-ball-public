using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Waves.Difficulty
{
    public class PlayerDifficultySystem : IEcsRunSystem
    {
        private readonly DifficultyService _difficulty;
        private readonly EcsFilterInject<Inc<OnSpawnedWave>> _onSpawnedWaveFilter = default;
        private readonly EcsFilterInject<Inc<Player.Player>> _playerFilter = default;

        public PlayerDifficultySystem(DifficultyService difficulty) => _difficulty = difficulty;

        public void Run(EcsSystems systems)
        {
            foreach (var iEvent in _onSpawnedWaveFilter)
            {
                var index = _onSpawnedWaveFilter.Pools.Inc1.Get(iEvent).Index;
                if (index == 0) continue;

                foreach (var iPlayer in _playerFilter)
                {
                    ref var player = ref _playerFilter.Pools.Inc1.Get(iPlayer);
                    _difficulty.ApplyDifficultyToPlayerIncremental(ref player);
                }
            }
        }
    }
}