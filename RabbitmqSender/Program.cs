

using RabbitMQ.Client;

ConnectionFactory factory=new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "RabbitMQ Sender App";

IConnection conn=factory.CreateConnection();

IModel channel=conn.CreateModel();
channel.Close();
conn.Close();
