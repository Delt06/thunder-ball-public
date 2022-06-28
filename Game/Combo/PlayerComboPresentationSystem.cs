using DELTation.LeoEcsExtensions.Components;
using Game._Data;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Combo
{
    public class PlayerComboPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Combo, Player.Player, UpdateEvent<Combo>>> _filter = default;
        private readonly PlayerComboView _playerComboView;

        public PlayerComboPresentationSystem(UiSceneData uiSceneData) => _playerComboView = uiSceneData.PlayerComboView;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var count = _filter.Pools.Inc1.Get(i).Count;
                _playerComboView.DisplayCombo(count);
            }
        }
    }
}