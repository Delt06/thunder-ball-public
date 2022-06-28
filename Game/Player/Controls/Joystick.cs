using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Player.Controls
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] [Required] private Image _circle;
        [SerializeField] [Required] private Image _handle;
        [SerializeField] private AnimationCurve _magnitudeCurve;
        private Vector2 _lastUnclampedOffset;
        private float _maxHandleDistance;

        private int? _pointerId;
        private Vector2 _pressPosition;

        public Vector2 Value => _pointerId == null ? Vector2.zero : CorrectedValue;

        private Vector2 CorrectedValue
        {
            get
            {
                var offset = GetOffset();
                var ratio = offset.magnitude / _maxHandleDistance;
                ratio = _magnitudeCurve.Evaluate(ratio);
                return offset.normalized * ratio;
            }
        }

        public Vector2 ValueUnclamped
        {
            get
            {
                if (_pointerId == null) return Vector2.zero;
                return _lastUnclampedOffset / _maxHandleDistance;
            }
        }

        private void Awake()
        {
            _maxHandleDistance = _handle.transform.localPosition.magnitude;
            Hide();
        }

        private void OnDisable()
        {
            Hide();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_pointerId != eventData.pointerId) return;

            var offset = eventData.position - _pressPosition;
            _lastUnclampedOffset = offset;
            offset = Vector2.ClampMagnitude(offset, _maxHandleDistance);
            _handle.transform.localPosition = offset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_pointerId != null) return;

            _pointerId = eventData.pointerId;
            _pressPosition = eventData.position;

            _circle.transform.position = eventData.position;
            _circle.enabled = true;

            _handle.transform.localPosition = Vector3.zero;
            _handle.enabled = true;
            PointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_pointerId != eventData.pointerId) return;
            _lastUnclampedOffset = Vector2.zero;
            var preReleaseValue = Value;
            Hide();
            PointerUp?.Invoke(preReleaseValue);
        }

        private Vector2 GetOffset() => _handle.transform.localPosition;

        public event Action PointerDown;
        public event Action<Vector2> PointerUp;

        private void Hide()
        {
            _pointerId = null;
            _circle.enabled = false;
            _handle.enabled = false;
        }
    }
}