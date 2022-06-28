using static UnityEngine.SceneManagement.SceneManager;

namespace Game.Levels
{
    public class LevelLoader
    {
        public void Reload()
        {
            var sceneIndex = GetActiveScene().buildIndex;
            LoadScene(sceneIndex);
        }
    }
}