using DELTation.LeoEcsExtensions.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Destruction
{
    public class EntityViewDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ViewBackRef, EntityDestructionCommand>> _filter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var view = _filter.Pools.Inc1.Get(i).View;
                view.Destroy();
            }
        }
    }
}