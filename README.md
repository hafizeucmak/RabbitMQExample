# RabbitMQ Producer-Consumer Example

This console application demonstrates a simple RabbitMQ producer-consumer scenario using the EasyNetQ library. The producer and consumer run in separate threads, allowing the user to continuously send messages and consume them.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed
- [RabbitMQ Server](https://www.rabbitmq.com/download.html) installed and running on `localhost` (default settings)
- [Docker desktop] (https://docs.docker.com/get-docker/)

  
Project Structure

Producer.cs: Contains the Producer class responsible for creating a RabbitMQ queue and sending messages.
Consumer.cs: Contains the Consumer class responsible for consuming messages from a RabbitMQ queue.
Program.cs: The main entry point of the application, where threads for producing and consuming messages are started.


Configuration
The RabbitMQ connection settings are configured in the ConnectionFactory instances in the Producer and Consumer classes. Adjust the Uri property according to your RabbitMQ server configuration.
