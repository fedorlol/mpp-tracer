using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private Result Result;
        public Result GetResult()
        {
            return Result;
        }

        public void StartTrace()
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.ReflectedType.Name;

            TraceMethod mTracer = new TraceMethod(methodName, className);
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TraceThread currentThreadTracer = Result.GetThreadTracer(threadId);

            if (currentThreadTracer == null)
            {
                currentThreadTracer = new TraceThread(threadId);
                currentThreadTracer = Result.AddThreadTracer(threadId, currentThreadTracer);
            }
            currentThreadTracer.StartTrace(mTracer);
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TraceThread threadTracer = Result.GetThreadTracer(threadId);
            if (threadTracer != null)
            {
                threadTracer.StopTrace();
            }
        }

        public Tracer()
        {
            Result = new Result();
        }

    }
}
