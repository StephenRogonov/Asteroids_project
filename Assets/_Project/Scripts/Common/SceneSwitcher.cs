using UnityEngine.SceneManagement;

namespace _Project.Scripts.Common
{
    public class SceneSwitcher
    {
        public const int GAME = 2;
        public const int MENU = 1;

        public void LoadGame()
        {
            SceneManager.LoadScene(GAME);
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(MENU);
        }
    }
}