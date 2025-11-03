using Newtonsoft.Json;

namespace VividOrange.Geometry.Serialization.Extensions
{
    public static class GeometryJsonSerializationExtensions
    {
        public static string ToJson<T>(this T profile) where T : IGeometryBase
        {
            return JsonConvert.SerializeObject(profile, Formatting.Indented, GeometryJsonSerializer.Settings);
        }

        public static T FromJson<T>(this string json) where T : IGeometryBase
        {
            return JsonConvert.DeserializeObject<T>(json, GeometryJsonSerializer.Settings);
        }
    }
}
