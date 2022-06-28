using DELTation.LeoEcsExtensions.Composition;
using Game.Loot.HealthIncrease;
using Leopotam.EcsLite;

namespace Game.Loot
{
    public class LootSystemBuilder : ISystemBuilder
    {
        private readonly LootAssetDatabase _lootAssetDatabase;

        public LootSystemBuilder(LootAssetDatabase lootAssetDatabase) => _lootAssetDatabase = lootAssetDatabase;

        public void Populate(EcsSystems systems)
        {
            _lootAssetDatabase.ForEachType(lootAsset => systems.Add(lootAsset.CreateLootSystem()));
        }
    }
}