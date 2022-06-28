using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Destruction;
using Game.Enemies.Models;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Animations
{
    public class DeathAnimationSystem : EcsSystemBase, IEcsRunSystem
    {
        private static readonly int IsDeadId = Animator.StringToHash("IsDead");
        private readonly EcsFilterInject<Inc<Animated, CreateDeadBody, ModelSlot, EntityDestructionCommand>>
            _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var animator = _filter.Pools.Inc1.Get(i).Animator;
                animator.SetBool(IsDeadId, true);

                var model = _filter.Pools.Inc3.Get(i).Model;
                model.transform.parent = null;

                ref var createDeadBody = ref _filter.Pools.Inc2.Get(i);
                ref var deadBody = ref World.NewEntityWith<DeadBody>();
                deadBody.GameObject = model;
                deadBody.Lifetime = createDeadBody.Lifetime;
                deadBody.HasLifetime = createDeadBody.HasLifetime;
            }
        }
    }
}