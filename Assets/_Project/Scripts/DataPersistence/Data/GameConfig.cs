using System;

namespace _Project.Scripts.Bootstrap.Configs
{
    [Serializable]
    public class GameConfig
    {
        public int AsteroidsPoolInitialSize { get; set; }
        public float AsteroidsSpawnRate { get; set; }
        public float AsteroidAngleOffset { get; set; }
        public int EnemiesPoolInitialSize { get; set; }
        public float EnemiesSpawnRate { get; set; }
        public float SpawnDistance { get; set; }
        public int LaserShotsStartCount { get; set; }
        public float LaserShotRestorationTime { get; set; }
        public float LaserBeamLifetime { get; set; }
        public float LaserDistance { get; set; }
        public int MissilesPoolInitialSize { get; set; }
        public float ShipAcceleration { get; set; }
        public float ShipMaxSpeed { get; set; }
        public float ShipRotationSpeed { get; set; }
        public string AndroidGameId { get; set; }
        public string IosGameId { get; set; }

        public GameConfig()
        {
            AsteroidsPoolInitialSize = 10;
            AsteroidsSpawnRate = 2;
            AsteroidAngleOffset = 20;
            EnemiesPoolInitialSize = 5;
            EnemiesSpawnRate = 10;
            SpawnDistance = 13;
            LaserShotsStartCount = 1;
            LaserShotRestorationTime = 10;
            LaserBeamLifetime = 0.2f;
            LaserDistance = 20;
            MissilesPoolInitialSize = 6;
            ShipAcceleration = 5;
            ShipMaxSpeed = 5;
            ShipRotationSpeed = 1;
            AndroidGameId = "5752796";
            IosGameId = "5752797";
        }
    }
}