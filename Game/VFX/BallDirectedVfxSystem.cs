using DELTation.LeoEcsExtensions.Components;
using Game.Ball;
using Game.Ball.DamagePosition;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.VFX
{
    public class BallDirectedVfxSystem : IEcsRunSystem
    {
        private readonly VisualEffectFactory _effectFactory;
        private readonly EcsFilterInject<Inc<OnBallDealtDamage, BallDamagePosition>> _eventFilter = default;
        private readonly EcsFilterInject<Inc<UnityRef<Transform>, Player.Player>> _playerFilter = default;

        public BallDirectedVfxSystem(VisualEffectFactory effectFactory) => _effectFactory = effectFactory;

        public void Run(EcsSystems systems)
        {
            foreach (var iEvent in _eventFilter)
            {
                var damagePosition = _eventFilter.Pools.Inc2.Get(iEvent).Position;

                foreach (var iPlayer in _playerFilter)
                {
                    var playerTransform = _playerFilter.Pools.Inc1.Get(iPlayer).Object;
                    var playerPosition = playerTransform.position;
                    var offset = damagePosition - playerPosition;
                    offset.y = 0f;
                    var effectEntity =
                        _effectFactory.CreatePooledAndThenReturn(playerPosition, ve => ve.DirectedElectricSpike);
                    var effect = effectEntity.Get().Vfx;
                    effect.transform.forward = offset;
                    var renderer = effect.GetComponent<ParticleSystemRenderer>();
                    renderer.lengthScale *= offset.magnitude;
                }
            }
        }
    }
}