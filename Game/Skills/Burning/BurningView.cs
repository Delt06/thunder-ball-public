using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Game.Skills.Burning
{
    public class BurningView : ComponentView<Burning>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            entity.GetOrAdd<OnStartedBurning>();
        }
    }
}