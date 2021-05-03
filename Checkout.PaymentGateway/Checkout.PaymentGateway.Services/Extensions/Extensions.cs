using System;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Services
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

        internal static string Mask(this string cardNumber)
        {
            var reg = new Regex(@"(?<=\d{4}\d{2})\d{2}\d{4}(?=\d{4})|(?<=\d{4}( |-)\d{2})\d{2}\1\d{4}(?=\1\d{4})");
            cardNumber = reg.Replace(cardNumber, new MatchEvaluator((m) => new String('*', m.Length)));
            return cardNumber;
        }

        private const string STREAMNOIO = "Cannot perform read/write from/to this stream.";
    }
}
