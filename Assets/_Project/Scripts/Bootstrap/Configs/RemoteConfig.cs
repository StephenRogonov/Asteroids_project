using Firebase.RemoteConfig;
using Newtonsoft.Json.Linq;

namespace _Project.Scripts.Bootstrap.Configs
{
    public class RemoteConfig
    {
        private string _gameConfigsJson;

        private int _asteroidsPoolInitialSize;
        private float _asteroidsSpawnRate;
        private float _asteroidAngleOffset;

        private int _enemiesPoolInitialSize;
        private float _enemiesSpawnRate;

        private float _spawnDistance;

        private int _laserShotsStartCount;
        private float _laserShotRestorationTime;
        private float _laserBeamLifetime;
        private float _laserDistance;

        private int _missilesPoolInitialSize;

        private float _shipAcceleration;
        private float _shipMaxSpeed;
        private float _shipRotationSpeed;

        private string _androidGameId;
        private string _iOSGameId;

        public int AsteroidsPoolInitialSize => _asteroidsPoolInitialSize;
        public float AsteroidsSpawnRate => _asteroidsSpawnRate;
        public float AsteroidAngleOffset => _asteroidAngleOffset;
        public int EnemiesPoolInitialSize => _enemiesPoolInitialSize;
        public float EnemiesSpawnRate => _enemiesSpawnRate;
        public float SpawnDistance => _spawnDistance;
        public int LaserShotsStartCount => _laserShotsStartCount;
        public float LaserShotRestorationTime => _laserShotRestorationTime;
        public float LaserBeamLifetime => _laserBeamLifetime;
        public float LaserDistance => _laserDistance;
        public int MissilesPoolInitialSize => _missilesPoolInitialSize;
        public float ShipAcceleration => _shipAcceleration;
        public float ShipMaxSpeed => _shipMaxSpeed;
        public float ShipRotationSpeed => _shipRotationSpeed;
        public string AndroidGameId => _androidGameId;
        public string IosGameId => _iOSGameId;

        public void ParseJson()
        {
            _gameConfigsJson = FirebaseRemoteConfig.DefaultInstance.GetValue("gameConfigs").StringValue;

            _asteroidsPoolInitialSize = (int)JObject.Parse(_gameConfigsJson)["asteroidsPoolInitialSize"];
            _asteroidsSpawnRate = (float)JObject.Parse(_gameConfigsJson)["asteroidsSpawnRate"];
            _asteroidAngleOffset = (float)JObject.Parse(_gameConfigsJson)["asteroidAngleOffset"];

            _enemiesPoolInitialSize = (int)JObject.Parse(_gameConfigsJson)["enemiesPoolInitialSize"];
            _enemiesSpawnRate = (float)JObject.Parse(_gameConfigsJson)["enemiesSpawnRate"];

            _spawnDistance = (float)JObject.Parse(_gameConfigsJson)["spawnDistance"];

            _laserShotsStartCount = (int)JObject.Parse(_gameConfigsJson)["laserShotsStartCount"];
            _laserShotRestorationTime = (float)JObject.Parse(_gameConfigsJson)["laserShotRestorationTime"];
            _laserBeamLifetime = (float)JObject.Parse(_gameConfigsJson)["laserBeamLifetime"];
            _laserDistance = (float)JObject.Parse(_gameConfigsJson)["laserDistance"];

            _missilesPoolInitialSize = (int)JObject.Parse(_gameConfigsJson)["missilesPoolInitialSize"];

            _shipAcceleration = (float)JObject.Parse(_gameConfigsJson)["shipAcceleration"];
            _shipMaxSpeed = (float)JObject.Parse(_gameConfigsJson)["shipMaxSpeed"];
            _shipRotationSpeed = (float)JObject.Parse(_gameConfigsJson)["shipRotationSpeed"];

            _androidGameId = (string)JObject.Parse(_gameConfigsJson)["androidGameId"];
            _iOSGameId = (string)JObject.Parse(_gameConfigsJson)["iOSGameId"];
        }
    }
}