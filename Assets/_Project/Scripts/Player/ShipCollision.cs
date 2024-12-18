using _Project.Scripts.Analytics;
using _Project.Scripts.Enemy;
using _Project.Scripts.Obstacles;
using System;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipCollision : MonoBehaviour
    {
        private AnalyticsEventManager _analyticsEventManager;

        public event Action Crashed;

        [Inject]
        private void Construct(AnalyticsEventManager analyticsEventManager)
        {
            _analyticsEventManager = analyticsEventManager;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>() != null || collision.gameObject.GetComponent<EnemyMovement>() != null)
            {
                _analyticsEventManager.LogEndGame();
                gameObject.SetActive(false);
                Crashed?.Invoke();
            }
        }
    }
}