using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Tracer
{
    public class Result
    {
        private ConcurrentDictionary<int, TraceThread> threadResults;

        internal ConcurrentDictionary<int, TraceThread> TraceThread
        {
            get
            {
                if (threadResults == null)
                {
                    threadResults = new ConcurrentDictionary<int, TraceThread>();
                }
                return threadResults;
            }
        }

        public List<TraceThread> ThreadsList
        {
            get
            {
                return new List<TraceThread>(TraceThread.Values).OrderBy(item => item.ThreadNum).ToList();
            }

        }

        internal TraceThread AddThreadTracer(int id, TraceThread value)
        {
            if (TraceThread.TryAdd(id, value))
            {
                return GetThreadTracer(id);
            }
            return null;
        }

        internal TraceThread GetThreadTracer(int id)
        {
            TraceThread threadTracer;
            if (TraceThread.TryGetValue(id, out threadTracer))
            {
                return threadTracer;
            }
            return null;
        }

    }
}