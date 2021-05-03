using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Threading.Tasks;

namespace Checkout.Merchant
{
    internal static class Extensions
    {
        internal static async Task<byte[]> Compress<T>(this T input)
        {
            await using var inStream = new MemoryStream();
            await inStream.Serialize(input);

            await using var outStream = new MemoryStream();
            await using var tinyStream = new GZipStream(outStream, CompressionMode.Compress);
            await inStream.CopyToAsync(tinyStream);

            tinyStream.Close();

            return outStream.ToArray();
        }

        internal static async Task Serialize<T>(this Stream stream, T objectToWrite)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanWrite)
                throw new NotSupportedException(STREAMNOIO);

            stream.Seek(0, SeekOrigin.Begin);

            await JsonSerializer.SerializeAsync(stream, objectToWrite);
            stream.Seek(0, SeekOrigin.Begin);
        }

        private const string STREAMNOIO = "Cannot perform read/write from/to this stream.";
    }
}
