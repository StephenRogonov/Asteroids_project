using System.Collections.Generic;

public interface IAnalyticsEvents
{
    void LogEventWithoutParameters(string eventName);
    void LogEventWithParameters(string eventName, Dictionary<string, int> parameters);
}
