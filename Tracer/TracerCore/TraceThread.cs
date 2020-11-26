using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [DataContract]
    public class TraceThread
    {
        private int threadNum;

        [DataMember(Name = "Id", Order = 0)]
        internal int ThreadNum { get; private set; }

        private Stack<TraceMethod> _Pending;
        private List<TraceMethod> _Ready;

        internal TraceThread(int num) => threadNum = num;

        [DataMember(Name = "Time", Order = 1)]
        public long Time
        {
            get
            {
                long time = 0;
                foreach (TraceMethod m in Ready)
                {
                    time += m.Time;
                }
                return time;
            }
            set { }
        }

        internal Stack<TraceMethod> Pending
        {
            get
            {
                if (_Pending == null)
                {
                    _Pending = new Stack<TraceMethod>();
                }
                return _Pending;
            }
        }

        [DataMember(Name = "Methods", Order = 2)]
        internal List<TraceMethod> Ready
        {
            get
            {
                if (_Ready == null)
                {
                    _Ready = new List<TraceMethod>();
                }
                return _Ready;
            }
        }

        private void PushNested(TraceMethod m)
        {
            if (Pending.Count > 0)
            {
                Pending.Peek().PushNested(m);
            }
            else
            {
                Ready.Add(m);
            }
            Pending.Push(m);
        }

        internal void StartTrace(TraceMethod m)
        {
            PushNested(m);
            m.StartTrace();
        }

        internal void StopTrace()
        {
            if (Ready.Count == 0)
            {
                throw new InvalidOperationException("Method doesn't exist");
            }
            TraceMethod popedMethod = Pending.Pop();
            popedMethod.StopTrace();
        }
    }
}
