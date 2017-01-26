using System;
using System.Collections.Generic;
using System.Threading;
using Mooboo.Utilities;

namespace MooBoo.Utilities
{
    public class Scheduler : Singleton<Scheduler>
    {
        #region Fields

        private readonly Dictionary<string, Timer> _timers = new Dictionary<string, Timer>();

        #endregion

        #region Methods

        public bool Schedule(string key, TimeSpan period, Action func)
        {
            if (_timers.ContainsKey(key))
            {
                return false;
            }
            var timer = new Timer((t) =>
            {
                func.Invoke();
            }, null, (int)period.TotalMilliseconds,(int) period.TotalMilliseconds);
            _timers.Add(key, timer);
            
            
            return true;
        }

        public bool Unschedule(string key)
        {
            if (!_timers.ContainsKey(key))
            {
                return false;
            }
            var timer = _timers[key];
            timer.Dispose();
            _timers.Remove(key);
            return true;
        }

        /// <summary>
        /// Checks if timer with given key is scheduled.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>1 - if yes and running, 0 if yes and idle, -1 if no</returns>
        public int IsScheduled(string key)
        {
            if (_timers.ContainsKey(key))
            {
                var timer = _timers[key];
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

    }
}
