using System;
using UnityEngine;

namespace _Project.Scripts.PlayerWeapons
{
    public class LaserRestoration : MonoBehaviour
    {
        [SerializeField] private float _shotRestorationTime;

        public float TimeToRestore { get; private set; }
        public int ShotsAmount { get; private set; }

        public event Action ShotsAmountChanged;

        private void Start()
        {
            UpdateShotsAmount(1);
            TimeToRestore = _shotRestorationTime;
        }

        private void Update()
        {
            TimeToRestore -= Time.deltaTime;

            if (TimeToRestore < 0)
            {
                UpdateShotsAmount(1);
                TimeToRestore = _shotRestorationTime;
            }
        }

        public void UpdateShotsAmount(int amount)
        {
            ShotsAmount += amount;

            ShotsAmountChanged?.Invoke();
        }
    }
}