using _Project.Scripts.Enemy;
using _Project.Scripts.Obstacles;
using System;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class ShipCollision : MonoBehaviour
    {
        public event Action Crashed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>() != null || collision.gameObject.GetComponent<EnemyMovement>() != null)
            {
                gameObject.SetActive(false);
                Crashed?.Invoke();
            }
        }
    }
}