using _Project.Scripts.PlayerWeapons;

namespace _Project.Scripts.Obstacles
{
    interface IDamageable
    {
        void TakeHit(WeaponType hitType);
    }
}