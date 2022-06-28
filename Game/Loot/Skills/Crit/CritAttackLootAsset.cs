using DELTation.LeoEcsExtensions.Views;
using Game.Loot.Assets;
using Game.Skills.Crit;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Skills.Crit
{
    [CreateAssetMenu(menuName = AssetPath + "Crit Attack")]
    public class CritAttackLootAsset : SkillLootAssetBase<CritAttack, CritAttackLoot>
    {
        [SerializeField] [HideLabel] [InlineProperty]
        private CritParams _critParams;

        public override int Level => _critParams.Level;

        protected override EntityView SelectPrefab(ILootPrefabs lootPrefabs) => lootPrefabs.CritAttack;

        protected override void InitLoot(ref CritAttackLoot loot)
        {
            loot.Params = _critParams;
        }

        public override IEcsSystem CreateLootSystem() => new CritAttackLootSystem();
    }
}