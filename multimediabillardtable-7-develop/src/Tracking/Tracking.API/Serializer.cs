using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.API
{
    internal static class Serializer
    {
        public static async Task<byte[]> Serialize<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                var json = JsonConvert.SerializeObject(data);
                var jsonBytes = Encoding.UTF8.GetBytes(json);

                await stream.WriteAsync(BitConverter.GetBytes(json.Length), 0, 4);
                await stream.WriteAsync(jsonBytes, 0, jsonBytes.Length);

                return stream.ToArray();
            }
        }

        public static async Task<T> Deserialize<T>(Stream stream)
        {
            var lengthBytes = await ReadFromStream(stream, 4);
            var length = BitConverter.ToInt32(lengthBytes, 0);

            var jsonBytes = await ReadFromStream(stream, length);
            var json = Encoding.UTF8.GetString(jsonBytes);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private static async Task<byte[]> ReadFromStream(Stream stream, int length)
        {
            var buf = new byte[length];
            var read = 0;
            while (read < length)
                read += await stream.ReadAsync(buf, read, length - read);
            return buf;
        }
    }
}