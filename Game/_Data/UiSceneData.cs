using Game.Combo;
using Game.Player.Controls;
using Game.Ui;
using Game.Ui.BallReturn;
using Game.Ui.NewSkill;
using Game.Ui.Stats;
using Game.Waves;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Data
{
    public class UiSceneData : MonoBehaviour
    {
        [SerializeField] [Required] private PlayerComboView _playerComboView;
        [SerializeField] [Required] private Joystick _joystick;
        [SerializeField] [Required] private ScreenManager _screenManager;
        [SerializeField] [Required] private WaveLabel _waveLabel;
        [SerializeField] [Required] private StatsView _statsView;
        [SerializeField] [Required] private NewSkillView _newSkillView;
        [SerializeField] [Required] private BallReturnButton _ballReturnButton;

        public PlayerComboView PlayerComboView => _playerComboView;

        public Joystick Joystick => _joystick;

        public ScreenManager ScreenManager => _screenManager;

        public WaveLabel WaveLabel => _waveLabel;

        public StatsView StatsView => _statsView;

        public NewSkillView NewSkillView => _newSkillView;

        public BallReturnButton BallReturnButton => _ballReturnButton;
    }
}