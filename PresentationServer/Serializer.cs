using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LogicServer;

namespace PresentationServer
{
    public abstract class Serializer
    {
        public abstract string Serialize<T>(T objectToSerialize);
        public abstract T Deserialize<T>(string message);

        public abstract string? GetResponseHeader(string message);
        public abstract string? GetCommandHeader(string message);

        public static Serializer Create()
        {
            return new JsonSerializer();
        }

    }

}
