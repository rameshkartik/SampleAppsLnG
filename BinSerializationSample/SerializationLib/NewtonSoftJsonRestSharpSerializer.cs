using System.IO;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace LandisGyr.CommandCenter.Common
{
    /// <summary>
    /// Implementation to allow using the JSON.NET serializer for resstsharp
    /// </summary>
    public class NewtonSoftJsonRestSharpSerializer : ISerializer, IDeserializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializer"></param>
        public NewtonSoftJsonRestSharpSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
            this.serializer.NullValueHandling = NullValueHandling.Ignore;
            this.serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
            this.serializer.TypeNameHandling = TypeNameHandling.Auto;
        }

        public string ContentType
        {
            get { return "application/json"; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RootElement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        /// <summary>
        /// Default serializer implementation
        /// </summary>
        public static NewtonSoftJsonRestSharpSerializer Default
        {
            get
            {
                return new NewtonSoftJsonRestSharpSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,

                });
            }
        }
    }
}
