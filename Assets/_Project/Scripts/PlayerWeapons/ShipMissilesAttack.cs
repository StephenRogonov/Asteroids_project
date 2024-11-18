using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ShipMissilesAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;
        
        private ShipShootingMissilesConfig _shipShootingMissilesConfig;
        private MissilesFactory _missilesFactory;

        [Inject]
        private void Construct(ShipShootingMissilesConfig config)
        {
            _shipShootingMissilesConfig = config;
        }

        private void Awake()
        {
            _missilesFactory = new MissilesFactory(_shipShootingMissilesConfig, _shootingPoint);
        }

        public void PerformShot()
        {
            _missilesFactory.GetMissile();
        }
    }
}