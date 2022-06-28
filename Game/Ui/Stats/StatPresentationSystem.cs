using DELTation.LeoEcsExtensions.Components;
using Game._Data;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ui.Stats
{
    public abstract class StatPresentationSystem<TStat> : IEcsRunSystem where TStat : struct
    {
        private readonly StatsView _statsView;
        [UsedImplicitly]
        protected readonly EcsFilterInject<Inc<TStat, UpdateEvent<TStat>, Player.Player>> Filter;

        protected StatPresentationSystem(UiSceneData ui) => _statsView = ui.StatsView;

        public void Run(EcsSystems systems)
        {
            foreach (var i in Filter)
            {
                ref readonly var stat = ref Filter.Pools.Inc1.Get(i);
                var statView = _statsView.GetOrAdd<TStat>();
                Present(stat, statView);
            }
        }

        protected abstract void Present(in TStat stat, StatView view);
    }
}