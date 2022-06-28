using Cysharp.Threading.Tasks;
using DELTation.UI.Screens;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class LoseScreen : MonoBehaviour, IScreen
    {
        [SerializeField] [Required] private GameScreen _gameScreen;
        [SerializeField] [Required] private Button _restartButton;

        public async UniTask OpenAsync()
        {
            _gameScreen.Open();
            await _restartButton.OnClickAsync();
        }
    }
}