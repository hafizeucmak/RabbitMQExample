using MultipleConsumerRabbitMQExample;
using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        var queueName = "demo-queue";
        var producer = new Producer();
        var consumer = new Consumer();

        producer.CreateQueue(queueName);

        // Start a thread for message production
        var producerThread = new Thread(() =>
        {
            var index = 100;
            while (true)
            {
                var message = new { Name = "Hello I'm Producer", Count = index };
                var serializedMessage = JsonConvert.SerializeObject(message);
                producer.SendMessageToQueue(queueName, serializedMessage);
                index--;

                Thread.Sleep(100);
            }
        });

        // Start a thread for message consumption
        var consumerThread1 = new Thread(() =>
        {
            while (true)
            {
                var consumedMessage = consumer.ConsumeMessage(queueName);

                if (!string.IsNullOrEmpty(consumedMessage))
                {
                    Console.WriteLine($"Consumer 1 :  {consumedMessage}");
                    Console.WriteLine(Environment.NewLine);
                }
            }
        });

        // Start a thread for message consumption
        var consumerThread2 = new Thread(() =>
        {
            while (true)
            {
                var consumedMessage = consumer.ConsumeMessage(queueName);

                if (!string.IsNullOrEmpty(consumedMessage))
                {
                    Console.WriteLine($"Consumer 2 :  {consumedMessage}");
                    Console.WriteLine(Environment.NewLine);
                }
            }
        });

        // Start both threads
        producerThread.Start();
        consumerThread1.Start();
        consumerThread2.Start();

        // Wait for the threads to finish (not likely in this case)
        producerThread.Join();
        consumerThread1.Join();
        consumerThread2.Join();

    }
}