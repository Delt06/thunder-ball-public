using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Game.Health.Bar
{
    public class HealthBarPlaceholderView : ComponentView<HealthBarPlaceholder>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            entity.Get<HealthBarPlaceholder>().Transform = transform;
        }
    }
}