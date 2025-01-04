using Northwind.Queue.Models; // To use ProductQueueMessage.
using RabbitMQ.Client; // To use ConnectionFactory.
using RabbitMQ.Client.Events; // To use AsyncEventingBasicConsumer
using System.Text.Json; // To use JsonSerializer.

namespace Northwind.Background.Workers;

public class QueueWorker : BackgroundService
{
    private readonly ILogger<QueueWorker> _logger;
    // RabbitMQ objects:
    private const string _queueNameAndRoutingKey = "product";
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly AsyncEventingBasicConsumer _consumer;

    public QueueWorker(ILogger<QueueWorker> logger)
    {
        _logger = logger;

        _factory = new() { HostName = "localhost" };
        _connection = _factory.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(queue: _queueNameAndRoutingKey, durable: false, exclusive: false, autoDelete: false);
        _consumer = new(_channel);
        _consumer.ReceivedAsync += (model, args) => {
            byte[] body = args.Body.ToArray();
            ProductQueueMessage? message = JsonSerializer.Deserialize<ProductQueueMessage>(body);
            if (message is not null) { _logger.LogInformation($"Received product: Id: {message.Product.ProductId}, Name: {message.Product.ProductName}, Message: {message.Text}."); }
            else { _logger.LogInformation($"Received unknown: {args.Body.ToArray()}."); }
            return Task.CompletedTask;
        };

        // Start consuming as messages arrive in the queue.
        _channel.BasicConsumeAsync(queue: _queueNameAndRoutingKey, autoAck: true, consumer: _consumer);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(3000, stoppingToken);
        }
    }
}
