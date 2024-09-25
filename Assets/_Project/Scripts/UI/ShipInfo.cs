using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class ShipInfo : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _shipMovement;
        [SerializeField] private TMP_Text _playerPositionText;
        [SerializeField] private TMP_Text _playerRotationText;
        [SerializeField] private TMP_Text _playerSpeedText;

        private void Update()
        {
            _playerPositionText.text = "Position: " + string.Format("{0}, {1}", Mathf.Round(_shipMovement.transform.position.x * 100f) / 100f,
                    Mathf.Round(_shipMovement.transform.position.y * 100f) / 100f);
            _playerRotationText.text = "Rotation: " + Convert.ToInt32(_shipMovement.transform.eulerAngles.z) % 360;
            _playerSpeedText.text = "Speed: " + Mathf.Round(Vector3.Magnitude(_shipMovement.velocity) * 100f) / 100f;
        }
    }
}