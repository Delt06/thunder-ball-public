using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views;
using Game._Data;
using Game.Loot.Assets;
using Game.Skills;
using Game.Ui.Stats;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Loot.Skills
{
    public abstract class SkillLootAssetBase<TSkill, TLoot> : LootAssetBase, ISkillLoot
        where TSkill : struct, ISkill
        where TLoot : struct

    {
        [SerializeField] [Required] private string _skillName = "Skill Name";

        public string SkillName => _skillName;

        public abstract int Level { get; }

        public bool IsOwnedBy(EcsPackedEntityWithWorld entity, out int level)
        {
            if (!entity.Has<TSkill>())
            {
                level = default;
                return false;
            }

            level = entity.Read<TSkill>().Level;
            return true;
        }

        public LootAssetBase AsAsset() => this;

        public sealed override void CreateAt(ILootPrefabs lootPrefabs, Vector3 position)
        {
            var entityView = CreateAt(SelectPrefab(lootPrefabs), position);
            var entity = entityView.GetOrCreateEntity();
            ref var loot = ref entity.Add<TLoot>();
            InitLoot(ref loot);
            entity.Add<SkillLootRef>().SkillLoot = this;
        }

        protected abstract EntityView SelectPrefab(ILootPrefabs lootPrefabs);

        protected abstract void InitLoot(ref TLoot loot);

        public override bool TryCreateStatPresentationSystem(UiSceneData ui, out IEcsSystem system)
        {
            system = new SkillStatPresentationSystem<TSkill>(ui, this);
            return true;
        }
    }
}