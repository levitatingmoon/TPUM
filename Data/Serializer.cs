using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public abstract class Serializer
    {
        public static string ItemToJSON(IItem item)
        {
            return JsonSerializer.Serialize(item);
        }

        public static IItem JSONToItem(string json)
        {
            return JsonSerializer.Deserialize<IItem>(json);
        }

        public static string StorageToJSON(List<IItem> items)
        {
            return JsonSerializer.Serialize(items);
        }

        public static List<IItem> JSONToStorage(string json)
        {
            return new List<IItem>(JsonSerializer.Deserialize<List<IItem>>(json)!);
        }
    }
}
