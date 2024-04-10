﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PresentationServer
{
    internal class JsonSerializer : Serializer
    {
        public override string Serialize<T>(T objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public override T Deserialize<T>(string message)
        {
            return JsonConvert.DeserializeObject<T>(message);
        }

        public override string? GetResponseHeader(string message)
        {
            JObject jObject = JObject.Parse(message);
            if (jObject.TryGetValue("Header", out JToken? value))
            {
                return (string)value;
            }

            return null;
        }

        public override string? GetCommandHeader(string message)
        {
            JObject jObject = JObject.Parse(message);
            if (jObject.ContainsKey("Header"))
            {
                return (string)jObject["Header"];
            }

            return null;
        }

    }
}
