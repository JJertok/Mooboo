using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace MooBoo.Utility
{
    public class Scheduler
    {
        #region Fields
        private static Scheduler _instance;
        private readonly Dictionary<string, DispatcherTimer> timers = new Dictionary<string, DispatcherTimer>();
        #endregion

        #region Methods

        public bool Schedule(string key, TimeSpan period, Action func)
        {
            if (timers.ContainsKey(key))
            {
                return false;
            }
            var timer = new DispatcherTimer();
            timers.Add(key, timer);
            timer.Interval = period;
            timer.Tick += (t, args) =>
            {
                func.Invoke();
            };
            timer.Start();
            return true;
        }

        public bool Unschedule(string key)
        {
            if (!timers.ContainsKey(key))
            {
                return false;
            }
            var timer = timers[key];
            timer.Stop();
            timers.Remove(key);
            return true;
        }

        /// <summary>
        /// Checks if timer with given key is scheduled.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>1 - if yes and running, 0 if yes and idle, -1 if no</returns>
        public int IsScheduled(string key)
        {
            if (timers.ContainsKey(key))
            {
                var timer = timers[key];
                if (timer.IsEnabled)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return -1;
        }

        #endregion

        #region Singleton

        public static Scheduler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Scheduler();
                }
                return _instance;
            }
        }
        #endregion
    }
}
