using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Notes.Api
{
    internal static class JsonFormatterConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
        }
    }
}