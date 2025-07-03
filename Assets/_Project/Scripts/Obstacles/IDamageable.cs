using _Project.Scripts.PlayerWeapons;

namespace _Project.Scripts.Obstacles
{
    public interface IDamageable
    {
        ObstacleType ObstacleType { get; }
        void TakeHit(WeaponType hitType);
        void DestroyObject();
    }
}