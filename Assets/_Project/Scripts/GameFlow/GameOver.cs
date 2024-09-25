using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.GameFlow
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _obstaclesSpawner;

        private void OnEnable()
        {
            _obstaclesSpawner.SetActive(false);
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}