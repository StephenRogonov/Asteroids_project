namespace _Project.Scripts.UI
{
    public class HudModel
    {
        private int _laserShotsCount;

        public int LaserShotsCount => _laserShotsCount;

        public void ChangeLaserShotsCount(int shots)
        {
            _laserShotsCount += shots;
        }
    }
}