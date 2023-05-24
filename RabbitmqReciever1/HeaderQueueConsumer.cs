using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqReciever1
{
    public static class HeaderQueueConsumer
    {
        public static void Consume(IModel channel)
        {

            var header = new Dictionary<string, object>
            {
                {"account","new" }
            };
            channel.ExchangeDeclare(HeaderExchangeName, ExchangeType.Headers);
            channel.QueueDeclare(HeaderQueue, durable: false, exclusive: false, autoDelete: false);
            channel.QueueBind(HeaderQueue, HeaderExchangeName, string.Empty, header);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, args) =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                var messgeInArray = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(messgeInArray);
                Console.WriteLine($"Message recieved by Reciever2: {message}");
                channel.BasicAck(args.DeliveryTag, multiple: false);

            };
            string consumerTag = channel.BasicConsume(HeaderQueue, autoAck: false, consumer: consumer);

            Console.ReadLine();

            channel.BasicCancel(consumerTag);
        }
    }
}
