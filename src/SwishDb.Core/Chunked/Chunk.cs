using System.Threading;
using System.Threading.Tasks;

namespace SwishDb.Core.Chunked
{
    internal class Chunk
    {
        private byte[] hack;   // HACK!

        /// <summary>
        /// Write the specified bytes to the file at the specified offset.
        /// </summary>
        /// <param name="offset">The offset within the chunk where the bytes will be written.</param>
        /// <param name="bytes">The bytes to write.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that can be awaited.</returns>
        public async Task WriteAsync(int offset, byte[] bytes, CancellationToken cancellationToken = default)
        {
            // TODO - implement this for real
            await Task.CompletedTask;
            hack = bytes;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<byte[]> ReadAsync(int offset, int length, CancellationToken cancellationToken = default)
        {
            // TODO - implement this for real
            await Task.CompletedTask;
            return hack;
        }
    }
}
