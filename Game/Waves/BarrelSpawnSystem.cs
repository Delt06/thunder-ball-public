using System.Collections.Generic;
using DG.Tweening;
using Game._Data;
using Game.Loot;
using Game.VFX;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Waves
{
    public class BarrelSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnSpawnedWave>> _filter = default;
        private readonly List<int> _points = new();
        private readonly SceneData _sceneData;
        private readonly StaticData _staticData;
        private readonly VisualEffectFactory _visualEffectFactory;

        public BarrelSpawnSystem(StaticData staticData, VisualEffectFactory visualEffectFactory, SceneData sceneData)
        {
            _staticData = staticData;
            _visualEffectFactory = visualEffectFactory;
            _sceneData = sceneData;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var _ in _filter)
            {
                var barrelSpawnPositionsRoot = _sceneData.BarrelSpawnPositionsRoot;
                _points.Clear();
                for (var i = 0; i < barrelSpawnPositionsRoot.childCount; i++)
                {
                    _points.Add(i);
                }

                _points.Shuffle();

                var barrelsCount = _staticData.BarrelsCountRange.GetRandomInRangeInclusive();
                for (var barrelIndex = 0; barrelIndex < barrelsCount; barrelIndex++)
                {
                    var pointIndex = _points[barrelIndex];
                    var position = barrelSpawnPositionsRoot.GetChild(pointIndex).position;
                    var rotation = RandomUtils.RandomRotationY();
                    var barrelSpawnAnimation =
                        Object.Instantiate(_staticData.BarrelSpawnAnimationPrefab, position, rotation);
                    var sequence = barrelSpawnAnimation.Play(_visualEffectFactory);
                    sequence.AppendCallback(() =>
                        {
                            Object.Destroy(barrelSpawnAnimation.gameObject);
                            Object.Instantiate(_staticData.BarrelPrefab, position, rotation);
                        }
                    );
                }
            }
        }
    }
}