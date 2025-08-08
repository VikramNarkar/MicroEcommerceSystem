using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Common.Messaging
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConfiguration _configuration;
        private readonly string _hostname;
        private readonly string _queueName;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
            _hostname = _configuration["RabbitMQ:HostName"] ?? "localhost";
            _queueName = _configuration["RabbitMQ:QueueName"] ?? "product-created-queue";
        }

        public async Task SendProductCreatedMessageAsync<T>(T message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _queueName, 
                durable: false, 
                exclusive: false, 
                autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: "", 
                routingKey: _queueName, 
                body: body);
        }
    }
}
