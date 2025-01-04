using Microsoft.Identity.Client;

namespace Northwind.Background.Workers;

public class TimerWorker : IHostedService, IAsyncDisposable
{
    private readonly ILogger<TimerWorker> _logger;
    private int _executionCount = 0;
    private Timer? _timer;
    private int _seconds = 5;

    public TimerWorker(ILogger<TimerWorker> logger)
    {
        _logger = logger;
    }

    private void DoWork(object? state)
    {
        int count = Interlocked.Increment(ref _executionCount);
        _logger.LogInformation($"{nameof(TimerWorker)} is working, execution count: {count:#,0}");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(TimerWorker)} is running.");
        _timer = new Timer(callback: DoWork, state: null, dueTime: TimeSpan.Zero, period: TimeSpan.FromSeconds(_seconds));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(TimerWorker)} is stopping.");
        _timer?.Change(dueTime: Timeout.Infinite, period: 0);
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (_timer is IAsyncDisposable asyncTimer) { await asyncTimer.DisposeAsync(); }
        _timer = null;
    }
}
