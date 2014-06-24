// ShortStuffApi
// User.cs

using MongoDB.Bson;
using Newtonsoft.Json;
using ShortStuffApi.Converter;

namespace ShortStuffApi.Models
{
    public class User
    {
        [JsonConverter(typeof (ObjectIdConverter))]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public string Tag { get; set; }
        public string Picture { get; set; }
    }
}
