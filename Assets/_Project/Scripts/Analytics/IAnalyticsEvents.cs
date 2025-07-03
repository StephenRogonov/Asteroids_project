using System.Collections.Generic;

namespace _Project.Scripts.Analytics
{
    public interface IAnalyticsEvents
    {
        void LogEventWithoutParameters(string eventName);
        void LogEventWithParameters(string eventName, Dictionary<string, int> parameters);
    }
}