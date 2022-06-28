using System;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Systems.Run;
using Game.Destruction;
using Game.Loot.Assets;
using Game.Loot.SmartDrop;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Game.Loot.Drop
{
    public class DropOnDestructionSystem : EcsSystemBase, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DropOnDestruction, UnityRef<Transform>, EntityDestructionCommand>>
            _filter = default;
        private readonly ILootPrefabs _lootPrefabs;
        private readonly EcsFilterInject<Inc<Player.Player>> _playerFilter = default;
        private readonly SmartDropService _smartDropService;

        public DropOnDestructionSystem(ILootPrefabs lootPrefabs, SmartDropService smartDropService)
        {
            _lootPrefabs = lootPrefabs;
            _smartDropService = smartDropService;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var dropOnDestruction = _filter.Pools.Inc1.Get(i);
                if (RandomUtils.TryProbability(dropOnDestruction.SkipProbability)) continue;

                foreach (var iPlayer in _playerFilter)
                {
                    var playerEntity = World.PackEntityWithWorld(iPlayer);
                    var loot = GetLoot(dropOnDestruction, playerEntity);
                    if (loot == null) continue;

                    var transform = _filter.Pools.Inc2.Get(i).Object;
                    loot.CreateAt(_lootPrefabs, transform.position);
                }
            }
        }

        [CanBeNull]
        private LootAssetBase GetLoot(in DropOnDestruction dropOnDestruction, EcsPackedEntityWithWorld player) =>
            dropOnDestruction.Mode switch
            {
                DropMode.Fixed => RandomUtils.GetRandomItemWeighted(dropOnDestruction.WeightedLoots, wl => wl.LootAsset,
                    wl => wl.Weight
                ),
                DropMode.Smart => _smartDropService.GetLoot(player),
                _ => throw new ArgumentOutOfRangeException(),
            };
    }
}