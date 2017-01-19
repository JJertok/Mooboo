using System;
using System.Collections.Generic;
using System.Threading;

namespace MooBoo.Utilities
{
    public class Scheduler
    {
        #region Fields
        private static Scheduler _instance;
        private readonly Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        #endregion

        #region Methods

        public bool Schedule(string key, TimeSpan period, Action func)
        {
            if (timers.ContainsKey(key))
            {
                return false;
            }
            var timer = new Timer((t) =>
            {
                func.Invoke();
            }, null, (int)period.TotalMilliseconds,(int) period.TotalMilliseconds);
            timers.Add(key, timer);
            
            
            return true;
        }

        public bool Unschedule(string key)
        {
            if (!timers.ContainsKey(key))
            {
                return false;
            }
            var timer = timers[key];
            timer.Dispose();
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
                throw new NotImplementedException();
                //if (timer.IsEnabled)
                if(true)
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
