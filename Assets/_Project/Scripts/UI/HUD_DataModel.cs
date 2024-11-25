namespace _Project.Scripts.UI
{
    public class HUD_DataModel
    {
        private HUD_View _hudView;

        private int _laserShotsAvailable;

        public bool CanShootLaser { get; private set; }

        public HUD_DataModel(HUD_View view)
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