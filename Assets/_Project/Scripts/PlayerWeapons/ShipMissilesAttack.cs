using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipMissilesAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private ShipMissilesConfig _shipShootingMissilesConfig;
        private MissilesFactory _missilesFactory;

        [Inject]
        private void Construct(MissilesFactory missilesFactory)
        {
            _missilesFactory = missilesFactory;
        }

        public void PerformShot()
        {
            _missilesFactory.GetMissile();
        }
    }
}