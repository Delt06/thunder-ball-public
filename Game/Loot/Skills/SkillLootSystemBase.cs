using DELTation.LeoEcsExtensions.Systems.Run;
using DELTation.LeoEcsExtensions.Utilities;
using Game.Loot.Collection;
using Game.Skills;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Loot.Skills
{
    public abstract class SkillLootSystemBase<TSkill, TLoot> : EcsSystemBase, IEcsRunSystem
        where TLoot : struct, IHasLevel where TSkill : struct, ISkill
    {
        [UsedImplicitly]
        protected readonly EcsFilterInject<Inc<TLoot, IsBeingCollected, SkillLootRef>> LootFilter;

        public void Run(EcsSystems systems)
        {
            foreach (var i in LootFilter)
            {
                ref readonly var loot = ref LootFilter.Pools.Inc1.Get(i);
                var collector = LootFilter.Pools.Inc2.Get(i).Collector;
                if (!collector.Unpack(out _, out var collectorIdx)) continue;

                var skillLoot = LootFilter.Pools.Inc3.Get(i).SkillLoot;

                if (Has<TSkill>(collectorIdx))
                {
                    ref var existingSkill = ref Modify<TSkill>(collectorIdx);
                    if (loot.Level <= existingSkill.Level) continue;

                    Apply(loot, ref existingSkill);
                    OnLootedSkill(skillLoot);
                }
                else
                {
                    ref var newSkill = ref ModifyOrAdd<TSkill>(collectorIdx);
                    Apply(loot, ref newSkill);
                    OnLootedSkill(skillLoot);
                }
            }
        }

        private void OnLootedSkill(ISkillLoot skillLoot)
        {
            World.NewEntityWith<OnLootedSkill>().SkillLoot = skillLoot;
        }

        protected abstract void Apply(in TLoot loot, ref TSkill skill);
    }
}