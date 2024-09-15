using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject obstaclesSpawner;

    private void OnEnable()
    {
        obstaclesSpawner.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
