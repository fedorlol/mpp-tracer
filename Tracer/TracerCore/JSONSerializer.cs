using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class JSONSerializer : ISerializer
    {
        private JsonSerializer jsonSerializer;

        private JsonSerializer JsonSerializer
        {
            get
            {
                if (jsonSerializer == null)
                {
                    jsonSerializer = new JsonSerializer();
                }
                return jsonSerializer;
            }
        }
        public void SerializeResult(Result result, Stream stream)
        {
            using (StreamWriter sw = new StreamWriter(stream))
            {
                using (JsonWriter writer = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
                {
                    JsonSerializer.Serialize(writer, result);
                }
            }
        }
    }
}
