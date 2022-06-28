using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ball
{
    public class BallSpeedCorrectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Ball>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var ball = ref _filter.Pools.Inc1.Get(i);
                var velocity = ball.Rigidbody.velocity;
                velocity = velocity.normalized * ball.Speed;
                ball.Rigidbody.velocity = velocity;
            }
        }
    }
}