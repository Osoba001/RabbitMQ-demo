using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigData.Constants
{
    public static class HelloWorldPublishing
    {
        public static string exchangeName = "DemoExchange";
        public static string routingKey = "Demo-routing-key";
        public static string queue = "DemoQueue";
        public static string Url = "amqp://guest:guest@localhost:5672";
    }
}
