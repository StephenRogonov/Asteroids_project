using UnityEngine.SceneManagement;

public class SceneSwitcher
{
    public const int GAME = 1;

    public void LoadGame()
    {
        SceneManager.LoadScene(GAME);
    }
}
