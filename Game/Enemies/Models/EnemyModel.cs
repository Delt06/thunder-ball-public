using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Enemies.Models
{
    public class EnemyModel : MonoBehaviour
    {
        [SerializeField] [Required] private Animator _animator;

        public Animator Animator => _animator;
    }
}