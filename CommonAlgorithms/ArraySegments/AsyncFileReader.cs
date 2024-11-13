using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CommonAlgorithms.ArraySegments
{

    /// <summary>
    /// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/using-async-for-file-access
    /// </summary>
    public sealed class FileReadArraySegmentBuilder : IArraySegmentBuilder<byte>
    {
        private readonly FileStream _sourceStream;

        public FileReadArraySegmentBuilder(FileStream sourceStream)
            => _sourceStream = sourceStream;

        async Task<IEnumerable<ArraySegment<byte>>> IArraySegmentBuilder<byte>.Build(int bufferSize)
        {
            IList<ArraySegment<byte>> result = new List<ArraySegment<byte>>();

            byte[] buffer = new byte[bufferSize];
            int numRead;
            while ((numRead = await _sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                result.Add(new ArraySegment<byte>(buffer, 0, numRead));
            }

            await Task.CompletedTask;

            return result;
        }
    }
}

