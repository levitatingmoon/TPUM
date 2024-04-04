using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Data
{
    public abstract class Serializer
    {
        public static string ItemToJSON(IItem item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public static IItem JSONToItem(string json)
        {
            return JsonConvert.DeserializeObject<Item>(json);
        }

        public static string StorageToJSON(List<IItem> items)
        {
            return JsonConvert.SerializeObject(items);
        }

        public static List<IItem> JSONToStorage(string json)
        {
            return new List<IItem>(JsonConvert.DeserializeObject<List<Item>>(json)!);
        }
    }
}
