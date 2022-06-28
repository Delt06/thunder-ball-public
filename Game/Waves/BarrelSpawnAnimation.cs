using DG.Tweening;
using Game.VFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Waves
{
    public class BarrelSpawnAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _buriedOffset = Vector3.down;
        [SerializeField] [Required] private Transform _movedObject;
        [SerializeField] [Min(0f)] private float _maxInitialDelay = 0.5f;
        [SerializeField] [Min(0)] private float _duration = 0.75f;
        [SerializeField] private Ease _ease = Ease.InOutQuad;

        private void OnDestroy()
        {
            this.DOKill();
        }

        public Sequence Play(VisualEffectFactory visualEffectFactory)
        {
            var initialLocalPosition = _movedObject.localPosition;
            _movedObject.localPosition = initialLocalPosition + _buriedOffset;

            return DOTween.Sequence()
                    .SetId(this).SetRecyclable(true)
                    .AppendInterval(Random.Range(0, _maxInitialDelay))
                    .AppendCallback(() =>
                        visualEffectFactory.CreatePooledAndThenReturn(transform.position, ve => ve.BarrelAppearance)
                    )
                    .Append(_movedObject.DOLocalMove(initialLocalPosition, _duration).SetEase(_ease))
                ;
        }
    }
}