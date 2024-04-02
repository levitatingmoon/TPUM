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
        public static string ItemToJSON(IShopItem item)
        {
            return JsonSerializer.Serialize(item);
        }

        public static IShopItem JSONToItem(string json)
        {
            return JsonSerializer.Deserialize<ShopItem>(json);
        }

        public static string StorageToJSON(List<IShopItem> items)
        {
            return JsonSerializer.Serialize(items);
        }

        public static List<IShopItem> JSONToStorage(string json)
        {
            return new List<IShopItem>(JsonSerializer.Deserialize<List<ShopItem>>(json));
        }
    }
}
