using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Destruction;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Loot.Collection
{
    public class LootCollectionEndSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Loot, IsBeingCollected>, Exc<EntityDestructionCommand>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                Add<EntityDestructionCommand>(i);
            }
        }
    }
}