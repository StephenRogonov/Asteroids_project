using _Project.Scripts.PlayerWeapons;
using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class LaserInfo : MonoBehaviour
    {
        [SerializeField] private LaserRestoration _laserRestoration;
        [SerializeField] private TMP_Text _restorationTimerText;
        [SerializeField] private TMP_Text _shotsAvailableText;

        private void OnEnable()
        {
            _laserRestoration.ShotsAmountChanged += UpdateShotsCounter;
        }

        private void OnDisable()
        {
            _laserRestoration.ShotsAmountChanged -= UpdateShotsCounter;
        }

        private void Update()
        {
            _restorationTimerText.text = "New Laser Shot: " + TimeSpan.FromSeconds(_laserRestoration.TimeToRestore).ToString("mm':'ss");
        }

        private void UpdateShotsCounter()
        {
            _shotsAvailableText.text = "Laser Shots: " + _laserRestoration.ShotsAmount.ToString();

            if (_laserRestoration.ShotsAmount > 0)
            {
                _shotsAvailableText.color = Color.yellow;
            }
            else
            {
                _shotsAvailableText.color = Color.white;
            }
        }
    }
}