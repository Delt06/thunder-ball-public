using DELTation.LeoEcsExtensions.Components;
using Game._Data;
using Game.Stun;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EnemyMovement, UnityRef<Transform>>> _filter = default;
        private readonly SceneData _sceneData;
        private readonly EcsPoolInject<IsStunnedNow> _stunnedPool = default;

        public EnemyMovementSystem(SceneData sceneData) => _sceneData = sceneData;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref var enemyMovement = ref _filter.Pools.Inc1.Get(i);
                var castleZ = _sceneData.Castle.position.z;
                var desiredZ = castleZ + enemyMovement.DesiredDistance;
                var transform = _filter.Pools.Inc2.Get(i).Object;
                var newPosition = transform.position;

                if (!_stunnedPool.Value.Has(i) && enemyMovement.AutoMovement)
                {
                    var motion = enemyMovement.Direction * (enemyMovement.Speed * Time.deltaTime);
                    newPosition += motion;
                }

                newPosition.z = Mathf.Max(newPosition.z, desiredZ);
                transform.position = newPosition;
                enemyMovement.ReachedDesiredDistance = transform.position.z <= desiredZ;
            }
        }
    }
}