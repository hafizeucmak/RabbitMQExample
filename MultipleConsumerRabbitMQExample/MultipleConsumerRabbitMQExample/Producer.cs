using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MultipleConsumerRabbitMQExample
{
    public class Producer : IDisposable
    {
        ConnectionFactory _connectionFactory;
        public Producer()
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        }


        public void CreateQueue(string queueName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                }
            }
        }

        public void SendMessageToQueue(string queueName, string message)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                   channel.BasicPublish("", queueName, false,null, body);
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
