using DG.Tweening;
using Game.Loot.Skills;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Ui.NewSkill
{
    public class NewSkillView : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float _fadeInTime = 0.25f;
        [SerializeField] [Min(0f)] private float _stayTime = 0.5f;
        [SerializeField] [Min(0f)] private float _fadeOutTime = 0.25f;
        [SerializeField] [Required] private Canvas _canvas;
        [SerializeField] [Required] private CanvasGroup _canvasGroup;
        [SerializeField] [Required] private TMP_Text _lootNameText;
        [SerializeField] [Required] private TMP_Text _lootLevelText;
        [SerializeField] [Required] private string _lootLevelFormat = "lv. {0:0}";

        private void Awake()
        {
            _canvas.enabled = false;
        }

        private void OnDestroy()
        {
            this.DOKill();
        }

        public void Show(ISkillLoot loot)
        {
            this.DOKill();
            _canvas.enabled = true;
            _canvasGroup.alpha = 0;

            _lootNameText.text = loot.SkillName;
            _lootLevelText.SetText(_lootLevelFormat, loot.Level);

            DOTween.Sequence().SetId(this).SetRecyclable(true)
                .Append(_canvasGroup.DOFade(1f, _fadeInTime))
                .AppendInterval(_stayTime)
                .Append(_canvasGroup.DOFade(0f, _fadeOutTime))
                .OnComplete(() => _canvas.enabled = false);
        }
    }
}