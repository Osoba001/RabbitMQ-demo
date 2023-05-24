

using RabbitMQ.Client;

ConnectionFactory factory = new()
{
    Uri = new Uri(Url),
    ClientProvidedName = "RabbitMQ Sender App",
    
};

IConnection conn=factory.CreateConnection();

IModel channel=conn.CreateModel();
channel.Close();
conn.Close();
