using System.Text.Json;

namespace Serializers;
public interface ISerializer
{
    IEnumerable<T>? DeserializeJson<T>(string fileName);
    Task<IEnumerable<T>?> DeserializeJsonAsync<T>(string fileName);
    IEnumerable<T>? DeserializeXml<T>(string fileName);
    Task<IEnumerable<T>?> DeserializeXmlAsync<T>(string fileName);
    void SerializeJson<T>(IEnumerable<T> collection, string fileName, JsonSerializerOptions? options = null);
    Task SerializeJsonAsync<T>(IEnumerable<T> collection, string fileName, JsonSerializerOptions? options = null);
    void SerializeXml<T>(IEnumerable<T> collection, string fileName);
    Task SerializeXmlAsync<T>(IEnumerable<T> collection, string fileName);
}
