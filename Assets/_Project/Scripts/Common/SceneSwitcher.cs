using UnityEngine.SceneManagement;

public class SceneSwitcher
{
    public const int BOOTSTRAP = 0;
    public const int GAME = 1;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
