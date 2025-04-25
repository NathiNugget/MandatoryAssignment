using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MandatoryAssignment.TracingClasses
{
    public class JsonTraceListener : System.Diagnostics.TraceListener
    {
        private StreamWriter sw;

        public JsonTraceListener()
        {
           
            
            sw = File.AppendText(Directory.GetCurrentDirectory() + "\\anders.json");
            sw.AutoFlush = true;
        }

        public override void Write(string? message)
        {
            sw.Write(JsonSerializer.Serialize<string>(message));
        }

        public override void WriteLine(string? message)
        {
            sw.WriteLine(JsonSerializer.Serialize<string>(message));
        }

    }
}
