using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqSender
{
    public static class TopicQueueProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
        {
            {"x-message-ttl",300000 }
        };
            channel.ExchangeDeclare(TopicExchangeName, ExchangeType.Topic, arguments: ttl);
            

            for (int i = 0; i < 60; i++)
            {
                string message = $"Hello RabbitMQ #{i}, From Kelly";
                byte[] messageInByte = Encoding.UTF8.GetBytes(message);
                Thread.Sleep(TimeSpan.FromSeconds(1));

                channel.BasicPublish(TopicExchangeName, TopicRoutingKey, basicProperties: null, messageInByte);
            }
        }
    }
}