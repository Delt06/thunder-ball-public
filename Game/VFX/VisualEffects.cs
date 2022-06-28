using Game.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.VFX
{
    [CreateAssetMenu]
    public class VisualEffects : ScriptableObject
    {
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _ballImpact;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _obstacleDestruction;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _deadBodyDestruction;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _devilTeleport;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _summon;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _burning;
        [SerializeField] [Required] [PrefabSelector]
        private GameObject _iceCube;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _crit;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _directedElectricSpike;
        [SerializeField] [Required] [PrefabSelector]
        private ParticleSystem _barrelAppearance;

        public ParticleSystem BallImpact => _ballImpact;

        public ParticleSystem ObstacleDestruction => _obstacleDestruction;

        public ParticleSystem DeadBodyDestruction => _deadBodyDestruction;

        public ParticleSystem DevilTeleport => _devilTeleport;

        public ParticleSystem Summon => _summon;

        public ParticleSystem Burning => _burning;

        public GameObject IceCube => _iceCube;

        public ParticleSystem Crit => _crit;

        public ParticleSystem DirectedElectricSpike => _directedElectricSpike;

        public ParticleSystem BarrelAppearance => _barrelAppearance;
    }
}