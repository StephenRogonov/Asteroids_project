using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class HudView : MonoBehaviour
    {
        [Header("Laser Info")]
        [SerializeField] private TMP_Text _restorationTimerText;
        [SerializeField] private TMP_Text _shotsAvailableText;

        [Header("Ship Info")]
        [SerializeField] private TMP_Text _playerPositionText;
        [SerializeField] private TMP_Text _playerRotationText;
        [SerializeField] private TMP_Text _playerSpeedText;

        public void DisplayLaserRestorationTime(string time)
        {
            _restorationTimerText.text = "New Laser Shot: " + time;
        }

        public void DisplayAvailableLaserShotsCount(int count)
        {
            _shotsAvailableText.text = "Laser Shots: " + count;
        }

        public void DisplayShipStats(string position, int rotation, float speed)
        {
            _playerPositionText.text = "Position: " + position;
            _playerRotationText.text = "Rotation: " + rotation;
            _playerSpeedText.text = "Speed: " + speed;
        }
    }
}