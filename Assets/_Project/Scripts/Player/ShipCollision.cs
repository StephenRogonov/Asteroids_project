using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.GameFlow;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Obstacles.Asteroids;
using _Project.Scripts.Obstacles.Enemy;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipCollision : MonoBehaviour
    {
        private AnalyticsEventManager _analyticsEventManager;
        private GameOverController _gameOverController;

        [Inject]
        private void Construct(AnalyticsEventManager analyticsEventManager)
        {
            _analyticsEventManager = analyticsEventManager;
        }

        public void SetGameOverController(GameOverController gameOverController)
        {
            _gameOverController = gameOverController;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>() != null || collision.gameObject.GetComponent<EnemyMovement>() != null)
            {
                _analyticsEventManager.LogEndGame();
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<IDamageable>().DestroyObject();
                _gameOverController.GameOver();
            }
        }
    }
}