using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

public class JsonSerializerHelper
{
    public void SerializeToJson(string filePath)
    {
        string? json = JsonConvert.SerializeObject(Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public T DeserializeFromJson<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }

    // public void SerializeToJson<T>(T data, string filePath)
    // {
    //     string? json = JsonConvert.SerializeObject(data, Formatting.Indented);
    //     File.WriteAllText(filePath, json);
    // }

    // public T DeserializeFromJson<T>(string filePath)
    // {
    //     string json = File.ReadAllText(filePath);
    //     return JsonConvert.DeserializeObject<T>(json);
    // }
}