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

        public static ItemDTO ToDTO(this IShopItem item)
        {
            return new ItemDTO(
                item.Id,
                item.Name,
                item.Type.ToString(),
                item.Price
            );
        }
    }
}
