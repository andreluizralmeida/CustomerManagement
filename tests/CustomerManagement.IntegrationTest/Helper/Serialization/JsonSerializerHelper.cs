namespace CustomerManagement.IntegrationTest.Helper.Serialization;

using System.Text.Json;

public static class JsonSerializerHelper
{
    public static JsonSerializerOptions DefaultSerialisationOptions => new JsonSerializerOptions
    {
        IgnoreNullValues = true
    };

    public static JsonSerializerOptions DefaultDeserialisationOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
}