using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "RabbitMQ Reciever2 App";

IConnection conn = factory.CreateConnection();

IModel channel = conn.CreateModel();
QueueConsumer.Consume(channel);
channel.Close();
conn.Close();
