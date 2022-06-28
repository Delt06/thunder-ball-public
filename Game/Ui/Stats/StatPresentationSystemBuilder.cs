using DELTation.LeoEcsExtensions.Composition;
using Game._Data;
using Game.Loot.HealthIncrease;
using Leopotam.EcsLite;

namespace Game.Ui.Stats
{
    public class StatPresentationSystemBuilder : ISystemBuilder
    {
        private readonly LootAssetDatabase _lootAssetDatabase;
        private readonly UiSceneData _ui;

        public StatPresentationSystemBuilder(UiSceneData ui, LootAssetDatabase lootAssetDatabase)
        {
            _ui = ui;
            _lootAssetDatabase = lootAssetDatabase;
        }

        public void Populate(EcsSystems systems)
        {
            systems
                .Add(new HealthStatPresentationSystem(_ui))
                .Add(new DamageStatPresentationSystem(_ui))
                ;

            _lootAssetDatabase.ForEachType(lootAsset =>
                {
                    if (lootAsset.TryCreateStatPresentationSystem(_ui, out var system))
                        systems.Add(system);
                }
            );
        }
    }
}