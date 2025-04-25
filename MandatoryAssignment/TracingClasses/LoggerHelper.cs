using System.Diagnostics;

namespace MandatoryAssignment.TracingClasses
{

    /// <summary>
    /// This class contains the tools for the user of the framework to enable tracing and add their own TraceListeners
    /// </summary>
    public class LoggerHelper
    {
        private static TraceSource _instance;
        private static LoggerHelper _loggerInstance;

        private LoggerHelper(string name)
        {
            _instance = new TraceSource(name, SourceLevels.All);
        }

        /// <summary>
        /// This method grants the instance of the Logger-class, hence enabling them
        /// </summary>
        /// <returns></returns>
        public static LoggerHelper GetInstance(string name)
        {
            if (_loggerInstance == null)
            {
                _loggerInstance = new LoggerHelper(name);
            }


            return _loggerInstance;
        }

        public static LoggerHelper GetInstance()
        {
            return _loggerInstance;
        }

        public TraceListener Add(TraceListener tl)
        {
            _instance.Listeners.Add(tl);
            return tl;
        }

        public SourceLevels SetFilter(SourceLevels sl)
        {

            _instance.Switch ??= new SourceSwitch("");
            _instance.Switch.Level = sl;
            return sl;
        }

        public void TraceEvent(TraceEventType @event, int id, string? message)
        {

            if (message == null) _instance.TraceEvent(@event, id);
            else _instance.TraceEvent(@event, id, message);

        }

        /// <summary>
        /// Override of TraceEvent which takes an Enum ID instead of integer ID which internally handles casting
        /// </summary>
        /// <param name="event">EventType to trace</param>
        /// <param name="id">ID for the Event</param>
        /// <param name="message">Message to trace</param>
        public void TraceEvent(TraceEventType @event, IDs id, string? message)
        {

            if (message == null) _instance.TraceEvent(@event, (int)id);
            else _instance.TraceEvent(@event, (int)id, message);

        }



    }
}
