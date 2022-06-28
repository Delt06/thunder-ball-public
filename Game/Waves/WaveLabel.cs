using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Waves
{
    public class WaveLabel : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _text;
        [SerializeField] [Required] private string _format = "Wave {0:0}";
        [SerializeField] [Min(0f)] private float _fadeTime = 0.25f;
        [SerializeField] [Min(0f)] private float _stayTime = 1f;

        private void OnDestroy()
        {
            this.DOKill();
        }

        public void Show(int waveIndex)
        {
            this.DOKill();
            gameObject.SetActive(true);
            _text.SetText(_format, waveIndex + 1);
            var color = _text.color;
            color.a = 0f;
            _text.color = color;
            DOTween.Sequence().SetId(this)
                .Append(_text.DOFade(1f, _fadeTime))
                .AppendInterval(_stayTime)
                .Append(_text.DOFade(0f, _fadeTime))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}