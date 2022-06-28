using DELTation.LeoEcsExtensions.Utilities;
using Game.Loot.Assets;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Loot.Healing
{
    [CreateAssetMenu(menuName = AssetPath + "Healing")]
    public class HealingLootAsset : LootAssetBase
    {
        [SerializeField] [Min(0)] private float _healAmount = 1;

        public override void CreateAt(ILootPrefabs lootPrefabs, Vector3 position)
        {
            var entityView = CreateAt(lootPrefabs.Healing, position);
            var entity = entityView.GetOrCreateEntity();
            entity.Add<HealingLoot>().HealAmount = _healAmount;
        }

        public override IEcsSystem CreateLootSystem() => new HealingLootSystem();
    }
}