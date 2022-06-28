using Game.Aim;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Animations.Player
{
    public class PlayerLaunchBallAnimation : IEcsRunSystem
    {
        private static readonly int LaunchedBallId = Animator.StringToHash("LaunchedBall");
        private static readonly int AttackTypeId = Animator.StringToHash("AttackType");
        private readonly EcsFilterInject<Inc<Animated, PlayerAnimationParams, OnLaunchedBall>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var animator = _filter.Pools.Inc1.Get(i).Animator;
                animator.SetTrigger(LaunchedBallId);

                var attackTypes = _filter.Pools.Inc2.Get(i).AttackTypes;
                var attackType = Random.Range(0, attackTypes);
                animator.SetFloat(AttackTypeId, attackType);
            }
        }
    }
}