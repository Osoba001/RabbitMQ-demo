using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigData.Constants
{
    public static class HelloWorldPublishing
    {
        public const string Url = "amqp://guest:guest@localhost:5672";

        
        public const string DirectExchangeName = "Direct-Exchange";
        public const string DirectRoutingKey = "Direct-routing-key";
        public const string DirectQueue = "Direct-Queue";


        public const string TopicExchangeName = "Topic-Exchange";
        public const string TopicRoutingKey = "topic.routekey.account.hello";
        public const string TopicQueue = "Topic-Queue";
    }
}
