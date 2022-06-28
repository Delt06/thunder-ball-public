using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Utils;

namespace Game.Skills.Burning
{
    public class BurningSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Burning, HealthData>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var burning = ref _filter.Pools.Inc1.Get(i);
                ref readonly var burningParams = ref burning.Params;

                if (burning.RemainingTimes == 0)
                {
                    _filter.Pools.Inc1.Del(i);
                    Add<OnStoppedBurning>(i);
                }
                else
                {
                    burning.ElapsedTime += Time.deltaTime;
                    if (burning.ElapsedTime < burningParams.DamagePeriod) continue;

                    burning.ElapsedTime -= burningParams.DamagePeriod;
                    burning.RemainingTimes--;
                    _filter.Pools.Inc2.Modify(i).Health -= burningParams.Damage;
                }
            }
        }
    }
}