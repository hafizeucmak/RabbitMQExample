using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultipleConsumerRabbitMQExample
{
    public class Consumer : IDisposable
    {
        ConnectionFactory _connectionFactory;
        public Consumer()
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        }

        public string ConsumeMessage(string queueName)
        {
            var message = string.Empty;

            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, e) =>
                    {

                        var body = e.Body.ToArray();
                        message = Encoding.UTF8.GetString(body);
                    };

                    channel.BasicConsume(queueName, true, consumer);
                }
            }

            return message;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
