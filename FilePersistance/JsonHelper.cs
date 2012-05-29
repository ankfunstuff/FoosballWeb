using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FilePersistance
{
    public static class JsonHelper
    {
        public static string Serialize<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            var ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.Default.GetString(ms.ToArray());
            ms.Dispose();
            return retVal;
        }

        public static T Deserialize<T>(string json) where T:new()
        {
            var obj = Activator.CreateInstance<T>();
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }

        public static dynamic Deserialize(string json, Type type)
        {
            var obj = Activator.CreateInstance(type);
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(obj.GetType());
            obj = serializer.ReadObject(ms);
            ms.Close();
            ms.Dispose();
            return obj;
        }
    }
}