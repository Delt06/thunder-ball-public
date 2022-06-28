using System.Collections.Generic;
using Game.Loot.Assets;
using Game.Loot.HealthIncrease;
using Game.Loot.Skills;
using Leopotam.EcsLite;

namespace Game.Loot.SmartDrop
{
    public class SmartDropService
    {
        private readonly LootAssetDatabase _lootAssetDatabase;
        private readonly List<LootAssetBase> _skills = new();
        private readonly SmartDropConfig _smartDropConfig;
        private readonly List<WeightedLoot> _weightedLoots = new();

        public SmartDropService(SmartDropConfig smartDropConfig, LootAssetDatabase lootAssetDatabase)
        {
            _smartDropConfig = smartDropConfig;
            _lootAssetDatabase = lootAssetDatabase;
        }

        public LootAssetBase GetLoot(EcsPackedEntityWithWorld skillOwner)
        {
            _skills.Clear();
            _weightedLoots.Clear();

            foreach (var lootDrop in _smartDropConfig.LootDrops)
            {
                _weightedLoots.Add(new WeightedLoot
                    {
                        Loot = lootDrop.Loot,
                        Weight = lootDrop.Weight,
                    }
                );
            }

            FindPossibleSkills(skillOwner);

            if (_skills.Count > 0)
            {
                var skillWeight = _smartDropConfig.TotalSkillsDropWeight / _skills.Count;
                foreach (var skill in _skills)
                {
                    _weightedLoots.Add(new WeightedLoot
                        {
                            Loot = skill,
                            Weight = skillWeight,
                        }
                    );
                }
            }

            var loot = RandomUtils.GetRandomItemWeighted(_weightedLoots, wl => wl.Loot, wl => wl.Weight);
            return loot;
        }

        private void FindPossibleSkills(EcsPackedEntityWithWorld skillOwner)
        {
            const int defaultSkillLevel = 0;

            foreach (var skillLoot in _lootAssetDatabase.Skills)
            {
                var currentSkillLevel = GetSkillLevelOrDefault(skillLoot, skillOwner, defaultSkillLevel);
                if (skillLoot.Level != currentSkillLevel + 1) continue;

                _skills.Add(skillLoot.AsAsset());
            }
        }


        private static int GetSkillLevelOrDefault(ISkillLoot skillLoot, EcsPackedEntityWithWorld skillOwner,
            int @default) =>
            skillLoot.IsOwnedBy(skillOwner, out var level) ? level : @default;

        private struct WeightedLoot
        {
            public LootAssetBase Loot;
            public float Weight;
        }
    }
}