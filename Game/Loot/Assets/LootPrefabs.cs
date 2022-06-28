using DELTation.LeoEcsExtensions.Views;
using Game.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Assets
{
    [CreateAssetMenu]
    public class LootPrefabs : ScriptableObject, ILootPrefabs
    {
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _healing;
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _damage;
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _healthIncrease;
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _burningAttackLoot;
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _freezingAttackLoot;
        [SerializeField] [Required] [PrefabSelector]
        private EntityView _critAttackLoot;

        public EntityView FreezingAttack => _freezingAttackLoot;

        public EntityView CritAttack => _critAttackLoot;

        public EntityView Healing => _healing;

        public EntityView Damage => _damage;

        public EntityView HealthIncrease => _healthIncrease;

        public EntityView BurningAttack => _burningAttackLoot;
    }
}