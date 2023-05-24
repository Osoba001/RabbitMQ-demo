using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new Uri(Url);
factory.ClientProvidedName = "RabbitMQ Reciever1 App";

IConnection conn = factory.CreateConnection();

IModel channel = conn.CreateModel();
DirectQueueConsumer.Consume(channel);

channel.Close();
conn.Close();
