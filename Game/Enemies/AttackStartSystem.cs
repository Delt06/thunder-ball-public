using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Enemies
{
    public class AttackStartSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Attack, EnemyMovement>, Exc<ActiveAttack, AttackCooldown, IsStunnedNow>>
            _filter =
                default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var reachedDesiredDistance = _filter.Pools.Inc2.Get(i).ReachedDesiredDistance;
                if (!reachedDesiredDistance) continue;

                Add<ActiveAttack>(i);
                Add<OnStartedAttack>(i);
            }
        }
    }
}