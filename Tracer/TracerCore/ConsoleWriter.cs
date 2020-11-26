using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    public class ConsoleWriter : IStreamWriter
    {
        public void Write(Result res, ISerializer serializer)
        {
            using (Stream @out = Console.OpenStandardOutput())
            {
                serializer.SerializeResult(res, @out);
            }
        }
    }
}
