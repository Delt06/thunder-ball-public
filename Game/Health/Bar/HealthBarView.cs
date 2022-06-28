using Plugins.SlicedFilledImage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Health.Bar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _transform;
        [SerializeField] [Required] private SlicedFilledImage _image;

        public Transform Transform => _transform;
        public void SetFill(float fill) => _image.fillAmount = fill;
    }
}