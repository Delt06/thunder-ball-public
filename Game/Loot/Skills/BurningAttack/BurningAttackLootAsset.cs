using DELTation.LeoEcsExtensions.Views;
using Game.Loot.Assets;
using Game.Skills.Burning;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Skills.BurningAttack
{
    [CreateAssetMenu(menuName = AssetPath + "Burning Attack")]
    public class BurningAttackLootAsset : SkillLootAssetBase<Game.Skills.Burning.BurningAttack, BurningAttackLoot>
    {
        [SerializeField] [HideLabel] [InlineProperty]
        private BurningParams _burningParams;

        public override int Level => _burningParams.Level;

        protected override EntityView SelectPrefab(ILootPrefabs lootPrefabs) => lootPrefabs.BurningAttack;

        protected override void InitLoot(ref BurningAttackLoot loot)
        {
            loot.Params = _burningParams;
        }

        public override IEcsSystem CreateLootSystem() => new BurningAttackLootSystem();
    }
}