using System.Text;
using System.Text.Json;
using Contracts.Messaging;
using InventoryService.Data;
using InventoryService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryService.Messaging
{
    public class ProductCreatedConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private IChannel _channel;

        public ProductCreatedConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
        }

        protected override async Task<Task> ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:HostName"] };
            using var connection = await factory.CreateConnectionAsync();
            _channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: _configuration["RabbitMQ:QueueName"],
                durable: false,
                exclusive: false,
                autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(_channel); 

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<ProductCreatedMessage>(json);

                Console.WriteLine($"Received ProductCreatedMessage for ProductId: {message?.ProductId}");

                if (message != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

                    var inventory = new Inventory
                    {
                        ProductId = message.ProductId,
                        QuantityInStock = 0,
                        LastUpdated = DateTime.UtcNow
                    };

                    context.Inventories.Add(inventory);
                    await context.SaveChangesAsync();
                }
            };

            await _channel.BasicConsumeAsync(queue: _configuration["RabbitMQ:QueueName"], autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            base.Dispose();
        }
    }
}
