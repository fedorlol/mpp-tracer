using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public interface IStreamWriter
    {
        void Write(Result result, ISerializer serializer);
    }
}
