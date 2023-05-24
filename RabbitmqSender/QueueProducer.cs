

using RabbitMQ.Client;
using System.Text;

public static class QueueProducer
{
    public static void Publish(IModel channel)
    {
        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false);
        channel.QueueBind(queue, exchangeName, routingKey);

        for (int i = 0; i < 60; i++)
        {
            string message = $"Hello RabbitMQ #{i}, From Kelly";
            byte[] messageInByte = Encoding.UTF8.GetBytes(message);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            channel.BasicPublish(exchangeName, routingKey, basicProperties: null, messageInByte);
        }
    }
}