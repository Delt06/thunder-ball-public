using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Obstacles
{
    public class ObstacleDestructionDetectionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Obstacle, EntityDestructionCommand, UnityRef<Transform>>>
            _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var transform = _filter.Pools.Inc3.Get(i).Object;
                World.NewEntityWith<OnObstacleDestroyed>().At = transform.position;
            }
        }
    }
}