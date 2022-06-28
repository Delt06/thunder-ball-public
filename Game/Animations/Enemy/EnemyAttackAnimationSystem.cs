using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Enemies;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Animations.Enemy
{
    public class EnemyAttackAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsAttackingId = Animator.StringToHash("IsAttacking");
        private static readonly int AttackTypeId = Animator.StringToHash("AttackType");
        private static readonly int AttackTimeId = Animator.StringToHash("AttackTime");

        private readonly EcsFilterInject<Inc<Animated, Attack, ActiveAttack>> _activeAttackFilter = default;
        private readonly EcsFilterInject<Inc<Animated, Attack>> _filter = default;
        private readonly EcsFilterInject<Inc<Animated, EnemyAnimationParams, OnStartedAttack>> _startFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var animator = _filter.Pools.Inc1.Get(i).Animator;
                var isAttacking = Has<ActiveAttack>(i);
                animator.SetBool(IsAttackingId, isAttacking);
            }

            foreach (var i in _startFilter)
            {
                var animator = _startFilter.Pools.Inc1.Get(i).Animator;
                var attackTypes = _startFilter.Pools.Inc2.Get(i).AttackTypes;
                var attackType = Random.Range(0, attackTypes);
                animator.SetFloat(AttackTypeId, attackType);
            }

            foreach (var i in _activeAttackFilter)
            {
                var animator = _activeAttackFilter.Pools.Inc1.Get(i).Animator;
                var duration = _activeAttackFilter.Pools.Inc2.Get(i).Duration;
                var elapsedTime = _activeAttackFilter.Pools.Inc3.Get(i).ElapsedTime;
                var attackTime = elapsedTime / duration;
                animator.SetFloat(AttackTimeId, attackTime);
            }
        }
    }
}