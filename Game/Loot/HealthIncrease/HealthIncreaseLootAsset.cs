using DELTation.LeoEcsExtensions.Utilities;
using Game.Loot.Assets;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Loot.HealthIncrease
{
    [CreateAssetMenu(menuName = AssetPath + "Health Increase")]
    public class HealthIncreaseLootAsset : LootAssetBase
    {
        [SerializeField] [Min(0)] private float _extraHealth = 5f;

        public override void CreateAt(ILootPrefabs lootPrefabs, Vector3 position)
        {
            var entityView = CreateAt(lootPrefabs.HealthIncrease, position);
            var entity = entityView.GetOrCreateEntity();
            entity.Add<HealthIncreaseLoot>().ExtraHealth = _extraHealth;
        }

        public override IEcsSystem CreateLootSystem() => new HealthIncreaseLootSystem();
    }
}