using System.Collections.Generic;
using System.Linq;

namespace ToggleSandbox.Services
{
    public class LogBuffer
    {
        #region Fields and Properties

        private Queue<Log> logs = new Queue<Log>();
        
        public bool IsEmpty
        {
            get
            {
                return logs.Count == 0;
            }
        }
        #endregion


        /// <summary>
        /// Add new Log entity with fragmentation another logs
        /// </summary>
        /// <param name="log"></param>
        public void Add(Log log)
        {
            var match = logs.FirstOrDefault(x => string.Equals(x.Name, log.Name));

            if (match == null)
            {
                logs.Enqueue(log);
                return;
            }

            
            var duration = log.Stop - log.Start;
            match.Stop = match.Stop.Add(duration);

            bool start = false;
            foreach (var item in logs)
            {
                if (!start && item == match)
                {
                    start = true;
                } else
                {
                    item.Start = item.Start.Add(duration);
                    item.Stop = item.Stop.Add(duration);
                }
            }
        }
        

        public Log Pop() {
            return logs.Dequeue();
        }
    }
}
