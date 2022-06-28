using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Ui.Stats
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _name;
        [SerializeField] [Required] private TMP_Text _info;
        [SerializeField] [Min(0f)] private float _scaleDuration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        private void OnDestroy()
        {
            this.DOKill();
        }

        public void Appear()
        {
            this.DOKill();
            transform.localScale = new Vector3(1, 0, 1);
            transform.DOScaleY(1f, _scaleDuration).SetEase(_ease).SetId(this);
        }

        public StatView UpdateName(string statName)
        {
            _name.text = statName;
            return this;
        }

        public StatView UpdateInfoFraction(float current, float max)
        {
            _info.SetText("{0:0}/{1:0}", current, max);
            return this;
        }

        public StatView UpdateInfoNumber(float number, int? decimalPlaces = null)
        {
            _info.SetTextFloat(number, decimalPlaces);
            return this;
        }

        public StatView UpdateInfoLevel(int level)
        {
            _info.SetText("Lv. {0:0}", level);
            return this;
        }
    }
}