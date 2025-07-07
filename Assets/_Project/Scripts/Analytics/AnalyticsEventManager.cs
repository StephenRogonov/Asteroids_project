using _Project.Scripts.Analytics;
using Firebase.Analytics;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts.Analytics
{
    public class AnalyticsEventManager : IAnalyticsEvents
    {
        private int _totalMissilesShot;
        private int _totalLaserShot;
        private int _totalAsteroidsDestroyed;
        private int _totalEnemiesDestroyed;

        public AnalyticsEventManager()
        {
            _totalMissilesShot = 0;
            _totalLaserShot = 0;
            _totalAsteroidsDestroyed = 0;
            _totalEnemiesDestroyed = 0;
        }

        public void LogEventWithoutParameters(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }

        public void LogEventWithParameters(string eventName, Dictionary<string, int> parameters)
        {
            Parameter[] pars = new Parameter[parameters.Count];

            for (int i = 0; i < parameters.Count; i++)
            {
                var item = parameters.ElementAt(i);
                pars[i] = new Parameter(item.Key, item.Value);
            }

            FirebaseAnalytics.LogEvent(eventName, pars);
        }

        public void LogEndGame()
        {
            Dictionary<string, int> parameters = new Dictionary<string, int>()
            {
                { LoggingEvents.MISSILES_TOTAL, _totalMissilesShot },
                { LoggingEvents.LASER_TOTAL, _totalLaserShot },
                { LoggingEvents.ASTEROIDS_DESTROYED, _totalAsteroidsDestroyed },
                { LoggingEvents.ENEMIES_DESTROYED, _totalEnemiesDestroyed }
            };

            LogEventWithParameters(LoggingEvents.END_GAME, parameters);
        }

        public void IncrementParameter(LogParameters paramName)
        {
            switch (paramName)
            {
                case LogParameters.MissilesShotsTotal:
                    _totalMissilesShot++;
                    break;
                case LogParameters.LaserShotsTotal:
                    _totalLaserShot++;
                    break;
                case LogParameters.AsteroidsDestroyedTotal:
                    _totalAsteroidsDestroyed++;
                    break;
                case LogParameters.EnemiesDestroyedTotal:
                    _totalEnemiesDestroyed++;
                    break;
                default:
                    break;
            }
        }
    }
}