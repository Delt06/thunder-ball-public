using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Loot.Animation
{
    public class LootAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<LootAnimation>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var lootAnimation = ref _filter.Pools.Inc1.Get(i);
                var y = lootAnimation.Curve.Evaluate(lootAnimation.ElapsedTime * lootAnimation.Speed);
                var localPosition = lootAnimation.Transform.localPosition;
                localPosition.y = y;
                lootAnimation.Transform.localPosition = localPosition;
                lootAnimation.ElapsedTime += Time.deltaTime;
            }
        }
    }
}