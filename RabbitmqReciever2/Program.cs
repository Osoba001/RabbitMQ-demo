using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "RabbitMQ Reciever2 App";

IConnection conn = factory.CreateConnection();

IModel channel = conn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "Demo-routing-key";
string queue = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false);
channel.QueueBind(queue, exchangeName, routingKey);
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
string consumerTag = channel.BasicConsume(queue, autoAck: false, consumer: consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
conn.Close();
