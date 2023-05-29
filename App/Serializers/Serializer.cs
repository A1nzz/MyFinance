using Serializers;
using System.Text.Json;
using System.Xml.Serialization;

namespace Serializers;
public class Serializer : ISerializer
{
    public void SerializeJson<T>(IEnumerable<T> collection, string fileName, JsonSerializerOptions? options = null)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
        {
            JsonSerializer.Serialize(fs, collection, options);
        }
    }

    public async Task SerializeJsonAsync<T>(IEnumerable<T> collection, string fileName, JsonSerializerOptions? options = null)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
        {
            await JsonSerializer.SerializeAsync(fs, collection, options);
        }
    }

    public IEnumerable<T>? DeserializeJson<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        return JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(fileName));
    }

    public async Task<IEnumerable<T>?> DeserializeJsonAsync<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<T>>(fs);
        }
    }

    public void SerializeXml<T>(IEnumerable<T> collection, string fileName)
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            XmlSerializer xmlSerializer = new(collection.GetType());
            xmlSerializer.Serialize(fs, collection);
        }
    }

    public async Task SerializeXmlAsync<T>(IEnumerable<T> collection, string fileName)
    {
        await Task.Run(() => SerializeXml(collection, fileName));
    }

    public IEnumerable<T>? DeserializeXml<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        using (FileStream fs = File.OpenRead(fileName))
        {
            XmlSerializer xmlSerializer = new(typeof(IEnumerable<T>));
            return (IEnumerable<T>?)xmlSerializer.Deserialize(fs);
        }

    }

    public async Task<IEnumerable<T>?> DeserializeXmlAsync<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }
        return await Task.Run(() => DeserializeXml<T>(fileName));
    }

}
