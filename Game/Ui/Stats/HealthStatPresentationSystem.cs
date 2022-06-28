using Game._Data;
using Game.Enemies;

namespace Game.Ui.Stats
{
    public class HealthStatPresentationSystem : StatPresentationSystem<HealthData>
    {
        public HealthStatPresentationSystem(UiSceneData ui) : base(ui) { }

        protected override void Present(in HealthData stat, StatView view)
        {
            view
                .UpdateName("HP")
                .UpdateInfoFraction(stat.Health, stat.MaxHealth)
                ;
        }
    }
}