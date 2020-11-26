using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Tracer
{
    [DataContract]
    class TraceMethod
    {
        private string _Name, _Class;
        private Stopwatch Stopwatch = new Stopwatch();
        private List<TraceMethod> _Nested = new List<TraceMethod>();

        internal TraceMethod(string mName, string cName)    
        {
            _Name = mName;
            _Class = cName;
            Stopwatch = new Stopwatch();
        }

        internal void StartTrace() => Stopwatch.Start();

        internal void StopTrace() => Stopwatch.Stop();

        [DataMember(Name = "Name", Order = 0)]
        public string Name
        {
            get
            {
                return _Name;
            }
            private set { }
        }

        [DataMember(Name = "Class", Order = 1)]
        public string ClassName
        {
            get
            {
                return _Class;
            }
            private set { }
        }

        public List<TraceMethod> GetNested() => _Nested;

        public void PushNested(TraceMethod m) => GetNested().Add(m);

        [DataMember(Name = "Time", Order = 2)]
        public long Time
        {
            get
            {
                if (!Stopwatch.IsRunning)
                {
                    return Stopwatch.ElapsedMilliseconds;
                }
                else
                {
                    throw new InvalidOperationException("Not ready yet");
                }
            }
            private set { }
        }
    }
}
