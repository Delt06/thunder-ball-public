using Game._Data;
using Game.Loot.Skills;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Ui.NewSkill
{
    public class NewSkillPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OnLootedSkill>> _filter = default;
        private readonly NewSkillView _view;

        public NewSkillPresentationSystem(UiSceneData ui) => _view = ui.NewSkillView;

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var skillLoot = _filter.Pools.Inc1.Get(i).SkillLoot;
                _view.Show(skillLoot);
            }
        }
    }
}