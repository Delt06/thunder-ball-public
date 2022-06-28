using DELTation.LeoEcsExtensions.Utilities;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Game.Enemies.Specific
{
    public class OscillatingMovementView : ComponentView<OscillatingMovement>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            entity.Get<OscillatingMovement>().StartX = entity.GetTransform().position.x;
        }
    }
}