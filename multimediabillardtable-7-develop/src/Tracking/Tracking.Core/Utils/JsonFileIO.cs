using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Tracking.Core.Utils
{
    internal static class JsonFileIO
    {
        /// <summary>
        /// Loads a JSON file from disk and deserializes it
        /// </summary>
        /// <typeparam name="T">Type to deserialize into</typeparam>
        /// <param name="path">Path to JSON file</param>
        /// <returns>Deserialized object</returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path))
                return default;

            var contents = File.ReadAllText(path, Encoding.UTF8);
            return JsonConvert.DeserializeObject<T>(contents);
        }

        /// <summary>
        /// Serializes an object and stores it in a JSON file
        /// </summary>
        /// <typeparam name="T">Type to serialize</typeparam>
        /// <param name="path">Path to JSON file</param>
        /// <param name="data">Object to store</param>
        public static void Store<T>(string path, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(path, json);
        }
    }
}
