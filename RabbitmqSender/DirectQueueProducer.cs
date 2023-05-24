

using RabbitMQ.Client;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public static class DirectQueueProducer
{
    public static void Publish(IModel channel)
    {
        var ttl=new Dictionary<string, object>
        {
            {"x-message-ttl",30000 }
        };
        channel.ExchangeDeclare(DirectExchangeName, ExchangeType.Direct,arguments: ttl);
        channel.QueueDeclare(DirectQueue, durable: false, exclusive: false, autoDelete: false);//
        channel.QueueBind(DirectQueue, DirectExchangeName, DirectRoutingKey);//

        for (int i = 0; i < 60; i++)
        {
            string message = $"Hello RabbitMQ #{i}, From Kelly";
            byte[] messageInByte = Encoding.UTF8.GetBytes(message);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            channel.BasicPublish(DirectExchangeName, DirectRoutingKey, basicProperties: null, messageInByte);
        }
    }
}