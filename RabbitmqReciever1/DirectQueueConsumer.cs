using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public static class DirectQueueConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(DirectExchangeName, ExchangeType.Direct);
        channel.QueueDeclare(DirectQueue, durable: false, exclusive: false, autoDelete: false);
        channel.QueueBind(DirectQueue, DirectExchangeName, DirectRoutingKey);

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) =>
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var messgeInArray = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(messgeInArray);
            Console.WriteLine($"Message recieved by Reciever1: {message}");
            channel.BasicAck(args.DeliveryTag, multiple: false);

        };
        string consumerTag = channel.BasicConsume(DirectQueue, autoAck: false, consumer: consumer);

        Console.ReadLine();

        channel.BasicCancel(consumerTag);
    }
}