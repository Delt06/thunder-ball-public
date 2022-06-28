using UnityEngine;
using static Game.Cameras.Shake.CameraShakePreset;

namespace Game.Cameras.Shake
{
    [CreateAssetMenu]
    public class CameraShakePresetsConfig : ScriptableObject
    {
        [SerializeField] private CameraShakePreset _damageBase = Standard;
        [SerializeField] private AnimationCurve _amplitudeOverDamage;
        [SerializeField] private CameraShakePreset _enemyDamage = Standard;

        public CameraShakePreset DamageBase => _damageBase;
        public AnimationCurve AmplitudeOverDamage => _amplitudeOverDamage;

        public CameraShakePreset EnemyDamage => _enemyDamage;
    }
}