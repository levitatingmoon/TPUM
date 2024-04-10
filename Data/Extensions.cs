using System;
using System.Text;

namespace Data
{
    internal static class Extensions
    {
        internal static ArraySegment<byte> GetArraySegment(this string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            return new ArraySegment<byte>(buffer);
        }

        public static ItemType ItemTypeFromString(string typeAsString)
        {
            return (ItemType)Enum.Parse(typeof(ItemType), typeAsString);
        }

        public static IItem ToItem(this ShopItem itemDTO)
        {
            return new Item(
                itemDTO.Name,
                itemDTO.Price,
                itemDTO.Id,
                (ItemType)itemDTO.Type
              
            );
        }
    }


}
