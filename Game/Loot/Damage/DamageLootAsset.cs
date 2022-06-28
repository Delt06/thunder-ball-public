using DELTation.LeoEcsExtensions.Utilities;
using Game.Loot.Assets;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Loot.Damage
{
    [CreateAssetMenu(menuName = AssetPath + "Damage")]
    public class DamageLootAsset : LootAssetBase
    {
        [SerializeField] [Min(0f)] private float _damageIncrease = 1f;

        public override void CreateAt(ILootPrefabs lootPrefabs, Vector3 position)
        {
            var entityView = CreateAt(lootPrefabs.Damage, position);
            var entity = entityView.GetOrCreateEntity();
            entity.Add<DamageLoot>().DamageIncrease = _damageIncrease;
        }

        public override IEcsSystem CreateLootSystem() => new DamageLootSystem();
    }
}