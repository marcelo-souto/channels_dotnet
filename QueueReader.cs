
namespace api_teste
{
    public class QueueReader(BackgroundQueueService<int> queue, ILogger<BackgroundQueueService<int>> logger) : BackgroundService
    {
        private readonly BackgroundQueueService<int> _queue = queue;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (await _queue.WaitForNextRead(cancellationToken))
            {
                var item = await _queue.DequeueAsync(cancellationToken);

                var wait = 4000;
                await Task.Delay(wait, cancellationToken);

                logger.LogInformation("Queue Reader: {item}", item);
                logger.LogInformation("Ação terminada em {wait} ms\n", wait);
            }
        }
    }
}