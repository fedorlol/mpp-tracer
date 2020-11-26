using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tracer
{
    public class FileWriter : IStreamWriter
    {
        private string fileName;
        public FileWriter(string fileN)
        {
            fileName = fileN;
        }
        public void Write(Result res, ISerializer serializer)
        {
            using (FileStream @out = new FileStream(fileName, FileMode.Append))
            {
                serializer.SerializeResult(res, @out);
            }
        }
    }
}
