using _Project.Scripts.Obstacles;
using _Project.Scripts.PlayerWeapons;
using System;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class ShipCollision : MonoBehaviour
    {
        public event Action Crashed;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable obstacle))
            {
                gameObject.SetActive(false);
                obstacle.TakeHit(HitType.Ship);
                Crashed?.Invoke();
            }
        }
    }
}