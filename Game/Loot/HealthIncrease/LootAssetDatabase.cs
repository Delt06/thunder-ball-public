using System;
using System.Linq;
using Game.Loot.Assets;
using Game.Loot.Skills;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Loot.HealthIncrease
{
    public class LootAssetDatabase
    {
        private const string Path = "Loot";

        public LootAssetDatabase()
        {
            All = Resources.LoadAll<LootAssetBase>(Path);
            OnePerType = All
                .GroupBy(r => r.GetType())
                .Select(g => g.First())
                .ToArray();
            Skills = All.OfType<ISkillLoot>().ToArray();
            Assert.IsTrue(All.Length > 0, "No loot assets found in " + Path);
        }

        private LootAssetBase[] All { get; }

        private LootAssetBase[] OnePerType { get; }

        public ISkillLoot[] Skills { get; }

        public void ForEachType(Action<LootAssetBase> action)
        {
            foreach (var asset in OnePerType)
            {
                action(asset);
            }
        }
    }
}