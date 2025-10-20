using _Project.Scripts.PlayerWeapons;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class ShipMissilesAttack : MonoBehaviour
    {
        [SerializeField] private Transform _shootingPoint;

        private MissilesFactory _missilesFactory;

        public void Init(MissilesFactory missilesFactory)
        {
            _missilesFactory = missilesFactory;
        }

        public void PerformShot()
        {
            _missilesFactory.GetMissile();
        }
    }
}