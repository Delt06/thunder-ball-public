using Game.Loot.Assets;
using Leopotam.EcsLite;

namespace Game.Loot.Skills
{
    public interface ISkillLoot
    {
        string SkillName { get; }
        int Level { get; }
        bool IsOwnedBy(EcsPackedEntityWithWorld entity, out int level);
        LootAssetBase AsAsset();
    }
}