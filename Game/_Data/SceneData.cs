using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Data
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] [Required] private Camera _camera;
        [SerializeField] [Required] private Transform _castle;
        [SerializeField] [Required] private Transform _waveSpawnPoint;
        [SerializeField] [Required] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] [Required] private Transform _barrelSpawnPositionsRoot;

        public Camera Camera => _camera;

        public Transform Castle => _castle;

        public Transform WaveSpawnPoint => _waveSpawnPoint;

        public CinemachineVirtualCamera VirtualCamera => _virtualCamera;

        public Transform BarrelSpawnPositionsRoot => _barrelSpawnPositionsRoot;
    }
}