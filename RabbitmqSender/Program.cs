

using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory=new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "RabbitMQ Sender App";

IConnection conn=factory.CreateConnection();

IModel channel=conn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "Demo-routing-key";
string queue = "DemoQueue";

channel.ExchangeDeclare(exchangeName,ExchangeType.Direct);
channel.QueueDeclare(queue,durable:false,exclusive:false,autoDelete:false);
channel.QueueBind(queue,exchangeName,routingKey);

for (int i = 0; i < 60; i++)
{
    string message = $"Hello RabbitMQ #{i}, From Kelly";
    byte[] messageInByte = Encoding.UTF8.GetBytes(message);
    Thread.Sleep(TimeSpan.FromSeconds(1));

    channel.BasicPublish(exchangeName, routingKey, basicProperties: null, messageInByte);
}

channel.Close();
conn.Close();
