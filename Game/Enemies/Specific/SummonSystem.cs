using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game._Data;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Enemies.Specific
{
    public class SummonSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EcsFilterInject<Inc<Summon, UnityRef<Transform>>, Exc<IsStunnedNow>> _filter = default;
        private readonly StaticData _staticData;

        public SummonSystem(StaticData staticData, EnemyFactory enemyFactory)
        {
            _staticData = staticData;
            _enemyFactory = enemyFactory;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var summon = ref _filter.Pools.Inc1.Get(i);
                summon.ElapsedTime += Time.deltaTime;
                if (summon.ElapsedTime < summon.Period) continue;

                summon.ElapsedTime -= summon.Period;
                var enemyPosition = _filter.Pools.Inc2.Get(i).Object.position;
                var distance = Random.Range(summon.DistanceRange.x, summon.DistanceRange.y);
                enemyPosition += Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * distance;
                var xLimit = _staticData.XLimit;
                enemyPosition.x = Mathf.Clamp(enemyPosition.x, -xLimit, xLimit);
                var entity = _enemyFactory.Create(summon.EnemyPreset, enemyPosition);
                World.NewEntityWith<OnSummoned>().SummonedEntity = entity;
            }
        }
    }
}