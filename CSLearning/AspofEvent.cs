using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSLearning.AspofEvent
{
    /**
     * 
    class AspofEvent
    {
     * example:
     *  http://www.akadia.com/services/dotnet_delegates_and_events.html#The%20very%20basic%20Delegate

     * 
     * components and mechanism of event model in C#
     * 1. Publisher
     *      the delegate definition(the handler/action form)
     *      field-like event property based on the delegate handler
     *      publishing the event in a process/function/procedure
     * 2. Subcriber
     *      subscription: hook up a connection between the action/handler and the event 
     *                      (exposed by the field-like event property)
     *      
     * usage:

     *  
     * the understanding context
     *  
     * 2.   event is a mechanism for communition of components within it own system
     * 
     *  
     * 1.   event is a wrapper for delegate that provides better encapsulation of delegate(prevent them 
     *      being accessed while a event should be dealt within the whole system and its inside mechanism, 
     *      and while it also provides interface for adding/removing whatever actions.
     *          source:
     *            why do we need the event modifier? 
     *            http://stackoverflow.com/questions/3028724/why-do-we-need-the-event-keyword-while-defining-events
     *
     * 3.   event is a code design pattern for object-oriented programming
     * 
     * 
     * Characteristics of Event on .NET Framework
     * 
     *  1. 
     *  2. 
     *  3. 
     *  4.
     *  5. 
     * 
    }
     */

    #region basics of event


    class MyClass
    {

        public delegate void LogHandler(string str);

        public event LogHandler LogEvent;
        //public LogHandler LogEvent;

        public void Process()
        {
            OnLog("Process() begins!");
            OnLog("Process() ends");
        }

        protected void OnLog(string message)
        {
            if (LogEvent != null)
            {
                LogEvent(message);
            }
        }
    }

    class FileLogger
    {
        public delegate void LogHandler(string str);

        public event LogHandler OnLogEventHandler;
        public event LogHandler AfterLogEventHandler;

        FileStream fileStream;
        StreamWriter streamWriter;

        public FileLogger(string filename)
        {
            fileStream = new FileStream(filename, FileMode.Append);
            streamWriter = new StreamWriter(fileStream);
        }

        public void Log(string s)
        {
            if (OnLogEventHandler != null)
            {
                OnLogEventHandler.Invoke("the Log event begins");
            }

            streamWriter.WriteLine(s);

            if (AfterLogEventHandler != null)
            {
                AfterLogEventHandler.Invoke("the Log event ends");
            }
        }

        public void Close()
        {
            streamWriter.Close();
            fileStream.Close();
        }
    }

    class TestAspofEvent
    {
        public TestAspofEvent()
        {

        }

        static void LogEventHandler(string s)
        {
            Console.WriteLine(s);
        }

        static void Main(string[] args)
        {
            FileLogger fLogger = new FileLogger("event-process.log");

            MyClass eventHandlerWrapper = new MyClass();

            //if (eventHandlerWrapper.LogEvent == null)
            //{
            //    Console.WriteLine("the log Event is null...");
            //}   
            

            eventHandlerWrapper.LogEvent += fLogger.Log;
            eventHandlerWrapper.LogEvent += LogEventHandler;

            //if (eventHandlerWrapper.LogEvent != null)
            //{
            //    eventHandlerWrapper.LogEvent("test");
            //}   

            eventHandlerWrapper.Process();
            
            fLogger.Close();
        }

    }
    #endregion

    #region a more explanatory example on Event Model 
    class TimeInfoEventArgs : EventArgs
    {
        public TimeInfoEventArgs(int hour, int min, int sec)
        {
            this.hour = hour;
            minute = min;
            second = sec;
        }
        public readonly int hour;
        public readonly int minute;
        public readonly int second;
    }

    // the 
    class Clock
    {
        private int _hour;
        private int _minute;
        private int _second;

        public delegate void SecondChangeHandler(object clock, TimeInfoEventArgs timeInfo);

        public event SecondChangeHandler SecondChange;

        protected void OnSecondChange(object clock, TimeInfoEventArgs timeInfo)
        {
            if (SecondChange != null)
            {
                SecondChange.Invoke(clock, timeInfo);
            }
        }

        public void Run()
        {
            for (; ; )
            {
                Thread.Sleep(1000);

                System.DateTime dt = System.DateTime.Now;

                if (dt.Second != _second)
                {
                    TimeInfoEventArgs ti = new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);
                    OnSecondChange(this, ti);
                }

                _second = dt.Second;
                _minute = dt.Minute;
                _hour = dt.Hour;
            }
        }
    }


    // Event Subscriber #1, that contains actions for the event
    class ClockTimeDisplay
    {
        public void Subscribe(Clock aClock)
        {
            aClock.SecondChange += DisplayBySecond;
        }

        public void DisplayBySecond(object clock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Current time: {0}:{1}:{2}", 
                ti.hour.ToString(), 
                ti.minute.ToString(), 
                ti.second.ToString()
                );
        }
    }

    // Event Subscriber #1, that contains actions for the event
    class ClockTimeLogger
    {
        public void Subscribe(Clock aClock)
        {
            aClock.SecondChange += LogClockTime;
        }
        public void LogClockTime(object theClock, TimeInfoEventArgs ti)
        {
            //
        }
    }

    class TestRealEventExample
    {

        // ===================================
        public static void Main(string[] args)
        {
            Clock aClock = new Clock();

            ClockTimeDisplay dis = new ClockTimeDisplay();
            dis.Subscribe(aClock);

            aClock.Run();
        }
	
    }

    #endregion
}
