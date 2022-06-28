using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Animations.Player
{
    public class PlayerMovementAnimationSystem : IEcsRunSystem
    {
        private static readonly int MovementDirectionId = Animator.StringToHash("MovementDirection");
        private readonly EcsFilterInject<Inc<Game.Player.Player, Animated, PlayerAnimationParams>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Pools.Inc1.Get(i);
                ref var animated = ref _filter.Pools.Inc2.Get(i);
                ref var animationParams = ref _filter.Pools.Inc3.Get(i);

                var targetMovementDirection = Mathf.Approximately(player.LastMovementDirection, 0f)
                    ? 0f
                    : Mathf.Sign(player.LastMovementDirection);
                var currentMovementDirection = animated.Animator.GetFloat(MovementDirectionId);
                currentMovementDirection = Mathf.SmoothDamp(currentMovementDirection, targetMovementDirection,
                    ref animationParams.MovementDirectionVelocity,
                    animationParams.MovementDirectionSmoothTime,
                    float.PositiveInfinity
                );
                animated.Animator.SetFloat(MovementDirectionId, currentMovementDirection);
            }
        }
    }
}