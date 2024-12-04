namespace _Project.Scripts.UI
{
    public class HudModel
    {
        private HudView _hudView;

        private int _laserShotsAvailable;

        public bool CanShootLaser { get; private set; }

        public HudModel(HudView view)
        {
            _hudView = view;

            _laserShotsAvailable = 0;
        }

        public void ChangeLaserShotsCount(int shots)
        {
            _laserShotsAvailable += shots;

            CanShootLaser = _laserShotsAvailable > 0;

            _hudView.DisplayAvailableLaserShotsCount(_laserShotsAvailable);
        }
    }
}