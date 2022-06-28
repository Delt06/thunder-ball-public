using System.Linq;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Game.Enemies;
using Game.Loot;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Waves
{
    public class WaveSpawnSystem : EcsSystemBase, IEcsRunSystem, IEcsInitSystem
    {
        private readonly EnemyFactory _factory;
        private readonly EcsFilterInject<Inc<EnemyWave, SpawnEnemyWaveCommand>> _filter = default;
        private readonly ProgressionConfig _progressionConfig;
        private readonly SceneData _sceneData;
        private readonly StaticData _staticData;
        private WavePreset[] _mainPresets;
        private WavePreset[] _startPresets;

        public WaveSpawnSystem(SceneData sceneData, EnemyFactory factory, StaticData staticData,
            ProgressionConfig progressionConfig)
        {
            _sceneData = sceneData;
            _factory = factory;
            _staticData = staticData;
            _progressionConfig = progressionConfig;
        }

        public void Init(EcsSystems systems)
        {
            _startPresets = _progressionConfig.StartPresets.ToArray();
            _startPresets.Shuffle();
            _mainPresets = _progressionConfig.MainPresets.ToArray();
            _mainPresets.Shuffle();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var spawnEnemyWaveCommand = ref _filter.Pools.Inc2.Get(i);
                spawnEnemyWaveCommand.Delay -= Time.deltaTime;
                if (spawnEnemyWaveCommand.Delay > 0f) continue;

                var waveIndex = _filter.Pools.Inc1.Get(i).Index;
                var waveSpawnPoint = _sceneData.WaveSpawnPoint;
                var wavePreset = SelectWavePreset(waveIndex);
                SpawnWave(wavePreset, waveSpawnPoint);

                _filter.Pools.Inc2.Del(i);
                World.NewEntityWith<OnSpawnedWave>().Index = waveIndex;
            }
        }

        private WavePreset SelectWavePreset(int waveIndex)
        {
            if (waveIndex < _startPresets.Length)
                return _startPresets[waveIndex];

            var mainWaveIndex = waveIndex - _startPresets.Length;
            if (mainWaveIndex < _mainPresets.Length)
                return _mainPresets[mainWaveIndex];
            return _mainPresets.GetRandomItem();
        }

        private void SpawnWave(WavePreset wavePreset, Transform at)
        {
            var origin = at.position;
            var xLimit = _staticData.XLimit;

            foreach (var enemyPlacement in wavePreset.EnemyPlacements)
            {
                var position = origin + Vector3.forward * enemyPlacement.Z;
                position.x = Mathf.Lerp(-xLimit, xLimit, enemyPlacement.X);
                _factory.Create(enemyPlacement.Enemy, position);
            }
        }
    }
}