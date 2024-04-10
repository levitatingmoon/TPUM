using System;
using System.Text;
using LogicServer;

namespace PresentationServer
{
    internal static class Extensions
    {
        internal static ArraySegment<byte> GetArraySegment(this string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(buffer);
        }

        public static ShopItem ToDTO(this IShopItem item)
        {
            return new ShopItem(          
                item.Name,
                item.Price,
                item.Id,
                item.Type
            );
        }
    }
}
