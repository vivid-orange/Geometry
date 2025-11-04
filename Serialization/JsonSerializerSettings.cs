using Newtonsoft.Json;

namespace VividOrange.Geometry.Serialization
{
    public static class GeometryJsonSerializer
    {
        public static JsonSerializerSettings Settings
        {
            get
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = {
                        new UnitsNet.Serialization.JsonNet.UnitsNetIQuantityJsonConverter(),
                    },
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                };
                return settings;
            }
        }
    }
}
