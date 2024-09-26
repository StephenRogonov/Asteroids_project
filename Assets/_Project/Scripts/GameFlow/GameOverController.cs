using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private ShipCollision _shipCollision;
    [SerializeField] private GameObject _obstaclesSpawner;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        _shipCollision.Crashed += GameOver;
    }

    private void GameOver()
    {
        _obstaclesSpawner.SetActive(false);
        _gameOverPanel.SetActive(true);
    }
}
