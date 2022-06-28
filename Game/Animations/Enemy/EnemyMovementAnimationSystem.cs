using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Animations.Enemy
{
    public class EnemyMovementAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsMovingId = Animator.StringToHash("IsMoving");
        private static readonly int MovementSpeedId = Animator.StringToHash("MovementSpeed");
        private readonly EcsFilterInject<Inc<EnemyMovement, Animated, EnemyAnimationParams>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var enemyMovement = ref _filter.Pools.Inc1.Get(i);
                var reachedDesiredDistance = enemyMovement.ReachedDesiredDistance;
                var animator = _filter.Pools.Inc2.Get(i).Animator;
                animator.SetBool(IsMovingId, !reachedDesiredDistance);

                ref var enemyAnimationParams = ref _filter.Pools.Inc3.Get(i);
                var movementSpeed = enemyMovement.Speed * enemyAnimationParams.MovementSpeedMultiplier;
                animator.SetFloat(MovementSpeedId, movementSpeed);
            }
        }
    }
}