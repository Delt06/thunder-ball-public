using DELTation.UI.Screens;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Ui
{
    public class GameplayScreen : MonoBehaviour, IScreen
    {
        [SerializeField] [Required] private GameScreen _gameScreen;

        public void Close() => _gameScreen.Close();
    }
}