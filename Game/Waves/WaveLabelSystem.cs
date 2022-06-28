using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Waves
{
    public class WaveLabelSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnSpawnedWave>> _filter = default;
        private readonly WaveLabel _waveLabel;

        public WaveLabelSystem(UiSceneData uiSceneData) => _waveLabel = uiSceneData.WaveLabel;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var waveIndex = _filter.Pools.Inc1.Get(i).Index;
                _waveLabel.Show(waveIndex);
            }
        }
    }
}