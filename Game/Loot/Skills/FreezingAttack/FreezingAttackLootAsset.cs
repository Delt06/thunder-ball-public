using DELTation.LeoEcsExtensions.Views;
using Game.Loot.Assets;
using Game.Skills.Freeze;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Skills.FreezingAttack
{
    [CreateAssetMenu(menuName = AssetPath + "Freezing Attack")]
    public class FreezingAttackLootAsset : SkillLootAssetBase<Game.Skills.Freeze.FreezingAttack, FreezingAttackLoot>
    {
        [SerializeField] [HideLabel] [InlineProperty]
        private FreezingParams _freezingParams;

        public override int Level => _freezingParams.Level;

        protected override EntityView SelectPrefab(ILootPrefabs lootPrefabs) => lootPrefabs.FreezingAttack;

        protected override void InitLoot(ref FreezingAttackLoot loot)
        {
            loot.Params = _freezingParams;
        }

        public override IEcsSystem CreateLootSystem() => new FreezingAttackLootSystem();
    }
}