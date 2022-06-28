using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Ball
{
    public class BallHorizontalVelocityPreventionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball, HorizontalVelocityPrevention>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var rigidbody = _filter.Pools.Inc1.Get(i).Rigidbody;
                var horizontalVelocityPrevention = _filter.Pools.Inc2.Get(i);
                var direction = rigidbody.velocity.normalized;
                var dot = Vector3.Dot(Vector3.right, direction);
                if (Mathf.Abs(dot) < horizontalVelocityPrevention.MinDot) continue;

                var angle = horizontalVelocityPrevention.FixRotation * (Random.value > 0.5f ? 1 : -1f);
                rigidbody.velocity = Quaternion.Euler(0, angle, 0) * rigidbody.velocity;
            }
        }
    }
}