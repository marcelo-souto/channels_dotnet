using System.Threading.Channels;

namespace api_teste
{
    public class BackgroundQueueService<T>
    {
        private readonly Channel<T> _channel = Channel.CreateUnbounded<T>(new UnboundedChannelOptions
        {
            SingleWriter = true,
            SingleReader = false
        });

        public bool Enqueue(T item)
        {
            return _channel.Writer.TryWrite(item);
        }

        public ValueTask<T> DequeueAsync(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.ReadAsync(cancellationToken);
        }

        public ValueTask<bool> WaitForNextRead(CancellationToken cancellationToken = default)
        {
            return _channel.Reader.WaitToReadAsync(cancellationToken);
        }
    }
}