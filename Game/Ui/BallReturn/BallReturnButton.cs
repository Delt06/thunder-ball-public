using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.BallReturn
{
    [RequireComponent(typeof(RectTransform))]
    public class BallReturnButton : MonoBehaviour
    {
        [SerializeField] [Required] private Button _button;
        [SerializeField] private Vector2 _hideOffset;
        [SerializeField] [Min(0f)] private float _showDuration = 0.5f;
        [SerializeField] private Ease _showEase = Ease.InOutQuad;
        private Vector2 _hiddenPosition;

        private RectTransform _rectTransform;
        private Vector2 _shownPosition;

        public Button.ButtonClickedEvent OnClick => _button.onClick;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _shownPosition = _rectTransform.anchoredPosition;
            _hiddenPosition = _shownPosition + _hideOffset;
            Hide();
        }

        private void OnDestroy()
        {
            this.DOKill();
        }

        public void Show()
        {
            this.DOKill();
            _rectTransform.DOAnchorPos(_shownPosition, _showDuration).SetEase(_showEase)
                .SetId(this);
        }

        public void Hide()
        {
            this.DOKill();
            _rectTransform.anchoredPosition = _hiddenPosition;
        }
    }
}