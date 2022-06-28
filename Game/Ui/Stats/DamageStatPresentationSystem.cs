using Game._Data;
using Game.Ball;

namespace Game.Ui.Stats
{
    public class DamageStatPresentationSystem : StatPresentationSystem<BaseBallDamage>
    {
        private const int DecimalPlaces = 2;
        public DamageStatPresentationSystem(UiSceneData ui) : base(ui) { }

        protected override void Present(in BaseBallDamage stat, StatView view)
        {
            view
                .UpdateName("DMG")
                .UpdateInfoNumber(stat.Damage, DecimalPlaces)
                ;
        }
    }
}